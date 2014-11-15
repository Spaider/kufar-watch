using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.UI
{
  public partial class FeedListForm : Form
  {
    private const string TRACE_CATEGORY = "FeedCheck";

    public FeedListForm()
    {
      InitializeComponent();
      RefreshFeeds();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var frm = new EditFeedDialog(new AdvWatcher.Feed());

      if (frm.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      using (var db = new AdvDb())
      {
        db.Feed.InsertWithIdentity(() =>
          new AdvWatcher.Feed
          {
            Caption = frm.Feed.Caption,
            Url = frm.Feed.Url
          });
      }
      RefreshFeeds();
    }

    private void RefreshFeeds()
    {
      using (var db = new AdvDb())
      {
        listBox1.DataSource = db.Feed.ToArray();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      var idList = new List<int>();
      foreach (var feed in from int idx in listBox1.SelectedIndices 
                           select listBox1.Items[idx] as AdvWatcher.Feed)
      {
        Debug.Assert(feed != null);
        idList.Add(feed.Id);
      }
      using (var db = new AdvDb())
      {
        db.Feed.Delete(f => idList.Contains(f.Id));
      }
      RefreshFeeds();
    }

    private void miExit_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void FeedListForm_Resize(object sender, EventArgs e)
    {
      switch (WindowState)
      {
        case FormWindowState.Minimized:
          notifyIcon1.Visible = true;
          Hide();
          break;

        case FormWindowState.Normal:
          notifyIcon1.Visible = false;
          break;
      }
    }

    private void notifyIcon1_DoubleClick(object sender, EventArgs e)
    {
      ShowInTaskbar = true;
      Show();
      WindowState = FormWindowState.Normal;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      AdvWatcher.Feed[] feeds;
      using (var db = new AdvDb())
      {
        feeds = db.Feed.ToArray();
      }
      if (feeds.Length == 0)
      {
        return;
      }
      foreach (var f in feeds)
      {
        var f1 = f;
        Task.Run(() => CheckFeed(f1.Id));
        Thread.Sleep(500);
      }
    }

    private static void CheckFeed(int id)
    {
      using (var db = new AdvDb())
      {
        var feed = db.Feed.FirstOrDefault(f => f.Id == id);
        if (feed == null)
        {
          Trace.WriteLine(string.Format("Feed {0} not found in DB", id), TRACE_CATEGORY);
          return;
        }
        Trace.WriteLine(string.Format("Check feed {0} - '{1}'", id, feed.Caption), TRACE_CATEGORY);
        var feedList = new AdvFeed(feed.Url);
        var lastTime = feed.LastAdTime.GetValueOrDefault();
        var checkTime = DateTime.Now;
        var advList = feedList
          .GetAdvs()
          .Where(a => a.Time > lastTime)
          .OrderByDescending(a => a.Time);

        if (advList.Any())
        {
          Trace.WriteLine(
            string.Format("Got {0} new ads for '{1}':", 
                          advList.Count(),
                          feed.Caption), 
            TRACE_CATEGORY);
          foreach (var adv in advList)
          {
            Trace.WriteLine(adv.Url);
          }
          var lastFeed = advList.First();
          Trace.WriteLine(string.Format("Last ad time for feed '{1}' is {0:G}", lastFeed.Time, feed.Caption));

          db.Feed
            .Where(f => f.Id == id)
            .Set(f => f.LastCheckTime, checkTime)
            .Set(f => f.LastAdTime, lastFeed.Time)
            .Update();
        }
        else
        {
          Trace.WriteLine(string.Format("No new ads for '{0}'", feed.Caption), TRACE_CATEGORY);
        }
      }
    }
  }
}
