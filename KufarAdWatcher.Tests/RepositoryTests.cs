using Dmitriev.AdWatcher.Kufar;
using NUnit.Framework;

namespace KufarAdWatcher.Tests
{
  [TestFixture]
  public class RepositoryTests
  {
    [Test]
    public void GetAdsTest()
    {
      const string URL = "http://www.kufar.by/минск_город/Для_дома_и_дачи/стиральная_машина--продается?cu=USD&sp=&ps=&pe=";
      var repo = new AdvRepository(URL);
      var adsList = repo.GetAdvs();

      Assert.IsNotEmpty(adsList);
    }
  }
}