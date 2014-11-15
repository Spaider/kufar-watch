using System;
using System.IO;
using System.Linq;
using Dmitriev.AdWatcher.DAL;
using Dmitriev.AdWatcher.Kufar;
using NUnit.Framework;

namespace KufarAdWatcher.Tests
{
  [TestFixture]
  public class RepositoryTests : BaseTest
  {
    [Test]
    public void SetLastCheckTime()
    {
      _testDbFileName = Path.GetTempFileName();
      AdvRepository.CreateDB(_testDbFileName);
      using (var repo = new AdvRepository(_testDbFileName))
      {
        var dt = repo.GetLastCheckTime();

        Assert.That(dt, Is.EqualTo(new DateTime()));

        var now = DateTime.Now;
        repo.SetLastCheckTime(now);

        dt = repo.GetLastCheckTime();
        Assert.That(dt, Is.EqualTo(now));
      }
    }

    [Test]
    public void SaveAds()
    {
      const string desc = "testdesc";
      var time = new DateTime(2013, 2, 23, 13, 34, 23);
      const string url = "http://some.sample.url/subfolder";

      _testDbFileName = Path.GetTempFileName();
      AdvRepository.CreateDB(_testDbFileName);
      using (var repo = new AdvRepository(_testDbFileName))
      {
        var adv = new AdvWatcher.Adv
        {
          Description = desc,
          IsRead = false,
          Time = time,
          Url = url
        };
        repo.SaveAdvs(new[]{adv});

        var advList = repo.GetAdvs().ToArray();

        Assert.NotNull(advList);
        Assert.That(advList.Length, Is.EqualTo(1));

        var advLoaded = advList.First();

        Assert.That(advLoaded.Description, Is.EqualTo(desc));
        Assert.That(advLoaded.Time, Is.EqualTo(time));
        Assert.That(advLoaded.Url, Is.EqualTo(url));
      }
    }
  }
}