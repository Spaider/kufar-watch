using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.UI
{
  public partial class FeedListForm : Form
  {
    private readonly IList<AdvWatcher.Feed>   _feeds            = new BindingList<AdvWatcher.Feed>();
    private readonly object                   _feedSync         = new object();
    private bool                              _checkInProgress;

    public FeedListForm()
    {
      InitializeComponent();
      listBox1.DataSource = _feeds;
      ReloadFeeds();
      timer1.Start();
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
      ReloadFeeds();
    }

    private void ReloadFeeds()
    {
      lock (_feedSync)
      {
        using (var db = new AdvDb())
        {
          _feeds.Clear();
          foreach (var feed in db.Feed)
          {
            _feeds.Add(feed);
          }
        }
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
      ReloadFeeds();
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

    private async void timer1_Tick(object sender, EventArgs e)
    {
      await CheckForNewFeeds();
    }

    private async Task CheckForNewFeeds()
    {
      if (_checkInProgress)
      {
        return;
      }
      _checkInProgress = true;
      AdvWatcher.Feed[] feedsCopy;
      lock (_feedSync)
      {
        feedsCopy = _feeds.ToArray();
      }
      var cnt = await Scheduler.CheckForNewAds(feedsCopy);
      Trace.WriteLine("Новых объявлений: " + cnt);
      _checkInProgress = false;
    }

    private async void FeedListForm_Load(object sender, EventArgs e)
    {
      await CheckForNewFeeds();
    }
  }
}
