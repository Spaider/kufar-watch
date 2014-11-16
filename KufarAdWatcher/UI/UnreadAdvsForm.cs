using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.UI
{
  public partial class UnreadAdvsForm : Form
  {
    private readonly IEnumerable<AdvWatcher.Feed> _feeds;
    private readonly IEnumerable<AdvWatcher.Adv>  _advs; 

    public UnreadAdvsForm(
      IEnumerable<AdvWatcher.Feed> feeds,
       IEnumerable<AdvWatcher.Adv> advs)
    {
      InitializeComponent();
      _feeds = feeds;
      _advs = advs;
      CustomInitializeComponent();
    }

    private void CustomInitializeComponent()
    {
      var groupMap = new Dictionary<int, ListViewGroup>();
      foreach (var feed in _feeds)
      {
        var group = new ListViewGroup
        {
          Header = feed.Caption,
          Name = feed.Caption,
          Tag = feed
        };
        groupMap[feed.Id] = group;
        listAdvs.Groups.Add(group);
      }
      foreach (var adv in _advs)
      {
        var group = groupMap[adv.FeedId];
        Debug.Assert(group != null);
        var listItem = new ListViewItem
        {
          Text = adv.Description,
          Tag = adv
        };
        listAdvs.Items.Add(listItem);
        group.Items.Add(listItem);
      }
      AutoSizeColumns();
    }

    private void listAdvs_DoubleClick(object sender, System.EventArgs e)
    {
      var pt = listAdvs.PointToClient(MousePosition);
      var item = listAdvs.GetItemAt(pt.X, pt.Y);
      if (item == null)
      {
        return;
      }
      var adv = item.Tag as AdvWatcher.Adv;
      if (adv == null)
      {
        return;
      }
      Process.Start(adv.Url);
    }

    private void UnreadAdvsForm_Resize(object sender, System.EventArgs e)
    {
      AutoSizeColumns();
    }

    private void AutoSizeColumns()
    {
      listAdvs.Columns[0].Width = ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4;
    }
  }
}
