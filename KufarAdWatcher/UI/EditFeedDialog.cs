using System.Windows.Forms;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.UI
{
  public partial class EditFeedDialog : Form
  {
    private readonly AdvWatcher.Feed _feed ;

    public EditFeedDialog(AdvWatcher.Feed feed)
    {
      InitializeComponent();

      _feed = feed;
      textBox1.Text = _feed.Url;
      textBox2.Text = _feed.Caption;
    }

    public AdvWatcher.Feed Feed
    {
      get { return _feed; }
    }

    private void textBox1_TextChanged(object sender, System.EventArgs e)
    {
      _feed.Url = textBox1.Text;
      CheckInput();
    }

    private void CheckInput()
    {
      button1.Enabled = !string.IsNullOrWhiteSpace(_feed.Url) && !string.IsNullOrWhiteSpace(_feed.Caption);
    }

    private void textBox2_TextChanged(object sender, System.EventArgs e)
    {
      _feed.Caption = textBox2.Text;
      CheckInput();
    }
  }
}
