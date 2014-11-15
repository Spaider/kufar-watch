using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.UI
{
  public partial class FeedListForm : Form
  {
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
  }
}
