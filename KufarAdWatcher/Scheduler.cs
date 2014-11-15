using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher
{
  public class Scheduler
  {
    private const string TRACE_CATEGORY = "Scheduler";

    public static Task<AdvWatcher.Adv[]> CheckFeed(int id)
    {
      return Task.Run(() => CheckFeedInternal(id));
    }

    public static async Task<int> CheckForNewAds(AdvWatcher.Feed[] feeds)
    {
      if (feeds == null || !feeds.Any())
      {
        return 0;
      }
      var newAds = new List<AdvWatcher.Adv>();
      foreach (var f in feeds)
      {
        var f1 = f;
        var ads = await CheckFeed(f1.Id);
        if (ads.Any())
        {
          newAds.AddRange(ads);
        }
      }
      return newAds.Count;
    }

    private static AdvWatcher.Adv[] CheckFeedInternal(int id)
    {
      using (var db = new AdvDb())
      {
        var feed = db.Feed.FirstOrDefault(f => f.Id == id);
        if (feed == null)
        {
          Trace.WriteLine(string.Format("Feed {0} not found in DB", id), TRACE_CATEGORY);
          return new AdvWatcher.Adv[0];
        }
        Trace.WriteLine(string.Format("Check feed {0} - '{1}'", id, feed.Caption), TRACE_CATEGORY);
        var feedList = new AdvFeed(feed.Url);
        var lastTime = feed.LastAdTime.GetValueOrDefault();
        var checkTime = DateTime.Now;
        var advList = feedList
          .GetAdvs()
          .Where(a => a.Time > lastTime)
          .OrderByDescending(a => a.Time);

        if (!advList.Any())
        {
          Trace.WriteLine(string.Format("No new ads for '{0}'", feed.Caption), TRACE_CATEGORY);
          return new AdvWatcher.Adv[0];
        }

        Trace.WriteLine(
            string.Format("Got {0} new ads for '{1}':",
                          advList.Count(),
                          feed.Caption),
            TRACE_CATEGORY);

        foreach (var adv in advList)
        {
          Trace.WriteLine(adv.Url);
        }
        var lastFeed = advList.First();
        Trace.WriteLine(string.Format("Last ad time for feed '{1}' is {0:G}", lastFeed.Time, feed.Caption));

        db.Feed
          .Where(f => f.Id == id)
          .Set(f => f.LastCheckTime, checkTime)
          .Set(f => f.LastAdTime, lastFeed.Time)
          .Update();

        return advList.ToArray();
      }
    }
  }
}