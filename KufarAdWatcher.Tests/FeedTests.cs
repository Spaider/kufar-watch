﻿using Dmitriev.AdWatcher.DAL;
using Dmitriev.AdWatcher.Feeds;
using NUnit.Framework;

namespace KufarAdWatcher.Tests
{
  [TestFixture]
  public class FeedTests : BaseTest
  {
    const string URL = "http://www.kufar.by/минск_город/Для_дома_и_дачи/стиральная_машина--продается?cu=USD&sp=&ps=&pe=";

    [Test]
    public void GetAdvs()
    {
      var feed = new Kufar(URL);
      var advList = feed.GetAdvs();

      Assert.IsNotEmpty(advList);
    } 
  }
}