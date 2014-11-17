using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;
using Dmitriev.AdWatcher.Properties;

namespace Dmitriev.AdWatcher.UI
{
  public partial class FeedListForm : Form
  {
    private const string TRAY_ICON_TEXT = "Объявления на Kufar.by";

    private readonly IList<AdvWatcher.Feed>   _feeds            = new BindingList<AdvWatcher.Feed>();
    private readonly object                   _feedSync         = new object();
    private bool                              _checkInProgress;
    private readonly IList<AdvWatcher.Adv>    _unreadAdvs       = new List<AdvWatcher.Adv>();

    public FeedListForm()
    {
      InitializeComponent();
      listBox1.DataSource = _feeds;
      ReloadFeeds();
      if (_feeds.Any())
      {
        UpdateTrayIconAndText();
        timer1.Start();
      }
      else
      {
        WindowState = FormWindowState.Normal;
      }
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

    private void RemoveFeeds()
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
      ShowNewFeedsDialog();
    }

    private async void timer1_Tick(object sender, EventArgs e)
    {
      await CheckForNewFeeds();
    }

    private async Task CheckForNewFeeds()
    {
      if (_checkInProgress || _feeds == null || !_feeds.Any())
      {
        return;
      }
      notifyIcon1.Icon = Resources.KufarSearch;
      _checkInProgress = true;
      AdvWatcher.Feed[] feedsCopy;
      lock (_feedSync)
      {
        feedsCopy = _feeds.ToArray();
      }
      AdvWatcher.Adv[] newAds = null;
      try
      {
        newAds = (await Scheduler.CheckForNewAds(feedsCopy)).ToArray();
      }
      catch (Exception e)
      {
        notifyIcon1.ShowBalloonTip(
          10000,
          "Что-то не так",
          "Ошибка при проверке объявлений",
          ToolTipIcon.Error);
        return;
      }
      _checkInProgress = false;

      if (newAds.Any())
      {
        foreach (var adv in newAds)
        {
          _unreadAdvs.Add(adv);
        }
        notifyIcon1.ShowBalloonTip(
          10000,
          "Новые объявления",
          "Новых объявлений: " + _unreadAdvs.Count,
          ToolTipIcon.Info);
        miNewFeeds.Enabled = true;
        UpdateTrayIconAndText();
        using (var stream = Resources.NewAdsAvailable)
        {
          var player = new SoundPlayer(stream);
          player.Play();
        }
      }
    }

    private void UpdateTrayIconAndText()
    {
      if (_unreadAdvs.Any())
      {
        notifyIcon1.Text = string.Format("{0}{1}Новых объявлений: {2}", TRAY_ICON_TEXT, Environment.NewLine,
          _unreadAdvs.Count);
        notifyIcon1.Icon = Resources.KufarExclamation;
      }
      else
      {
        notifyIcon1.Text = TRAY_ICON_TEXT + Environment.NewLine + "Новых объявлений нет";
        notifyIcon1.Icon = Resources.Kufar;
      }
    }

    private async void FeedListForm_Load(object sender, EventArgs e)
    {
      await CheckForNewFeeds();
    }

    private void tsbAddFeed_Click(object sender, EventArgs e)
    {
      timer1.Stop();
      ShowAddFeedDialog();
      if (_feeds.Any())
      {
        timer1.Start();
      }
    }

    private void ShowAddFeedDialog()
    {
      var text = Clipboard.GetText();
      var feed = new AdvWatcher.Feed();
      if (!string.IsNullOrEmpty(text) && Uri.IsWellFormedUriString(text, UriKind.Absolute))
      {
        feed.Url = text.Trim();
      }
      var frm = new EditFeedDialog(feed);

      if (frm.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      using (var db = new AdvDb())
      {
        var now = DateTime.Now;
        db.Feed.InsertWithIdentity(() =>
          new AdvWatcher.Feed
          {
            Caption = frm.Feed.Caption,
            Url = frm.Feed.Url,
            LastAdTime = now,
            LastCheckTime = now
          });
      }
      ReloadFeeds();
    }

    private void tsbRemoveFeed_Click(object sender, EventArgs e)
    {
      RemoveFeeds();
      if (!_feeds.Any())
      {
        timer1.Stop();
      }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      tsbRemoveFeed.Enabled = listBox1.SelectedIndices.Count > 0;
    }

    private void miFeedList_Click(object sender, EventArgs e)
    {
      ShowInTaskbar = true;
      Show();
      WindowState = FormWindowState.Normal;
    }

    private void miNewFeeds_Click(object sender, EventArgs e)
    {
      ShowNewFeedsDialog();
    }

    private void ShowNewFeedsDialog()
    {
      if (!_unreadAdvs.Any())
      {
        return;
      }
      var frm = new UnreadAdvsForm(_feeds.ToArray(), _unreadAdvs);
      if (frm.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      _unreadAdvs.Clear();
      miNewFeeds.Enabled = false;
      UpdateTrayIconAndText();
    }

    private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
    {
      ShowNewFeedsDialog();
    }
  }
}
