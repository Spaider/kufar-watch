using System;
using System.Linq;
using System.Windows.Forms;
using Dmitriev.AdWatcher.Kufar;

namespace Dmitriev.AdWatcher
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      const string URL = "http://www.kufar.by/минск_город/Для_дома_и_дачи/стиральная_машина--продается?cu=USD&sp=&ps=&pe=";
      var repo = new AdvRepository(URL);
//      var advs = repo.GetAdvs();
//      repo.SaveAdvs(advs);
//      advs = repo.GetLatestAdvs().ToArray();
//      Console.WriteLine(advs.Count());
      repo.MarkAsRead(new[]{1, 3, 5, 7});
    }
  }
}
