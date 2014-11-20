using System.Linq;
using Dmitriev.AdWatcher.Feeds;
using NUnit.Framework;

namespace KufarAdWatcher.Tests
{
  [TestFixture]
  public class FeedTests : BaseTest
  {
    // const string URL = "http://www.kufar.by/минск_город/Для_дома_и_дачи/стиральная_машина--продается?cu=USD&sp=&ps=&pe=";
    const string URL = "http://www.kufar.by/%D0%BC%D0%B8%D0%BD%D1%81%D0%BA_%D0%B3%D0%BE%D1%80%D0%BE%D0%B4%2F%D1%81%D1%82%D0%B8%D1%80%D0%B0%D0%BB%D1%8C%D0%BD%D0%B0%D1%8F_%D0%BC%D0%B0%D1%88%D0%B8%D0%BD%D0%B0--%D0%BF%D1%80%D0%BE%D0%B4%D0%B0%D0%B5%D1%82%D1%81%D1%8F%3Fcu%3DUSD%26sp%3D%26ps%3D%26pe%3D";

    [Test]
    public void GetAdvs()
    {
      var feed = new Kufar(URL);
      var advList = feed.GetAdvs();

      Assert.IsNotEmpty(advList);

      var adv = advList.First();

      Assert.IsNotNull(adv);
      Assert.IsNotNull(adv.Description);
      Assert.IsNotNull(adv.Url);
      Assert.That(adv.Price, Is.GreaterThan(0));
    } 
  }
}