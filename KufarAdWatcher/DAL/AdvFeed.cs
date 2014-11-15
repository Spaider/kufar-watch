﻿using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Dmitriev.AdWatcher.DAL
{
  public class AdvFeed
  {
    private readonly string _url;

    public AdvFeed(string url)
    {
      if (String.IsNullOrWhiteSpace(url))
        throw new ArgumentNullException("url");

      _url = url;
    }

    public IEnumerable<AdvWatcher.Adv> GetAdvs()
    {
      var web = new HtmlWeb();
      var doc = web.Load(_url);
      var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"list_item_thumbs\"]/article");

      return nodes.Select(CreateAdFromNode);
    }

    private static AdvWatcher.Adv CreateAdFromNode(HtmlNode node)
    {
      var timeNode = node.SelectSingleNode("time");
      var timeStr = timeNode.Attributes["datetime"].Value;

      var descNode = node.SelectSingleNode("div[2]/a[1]");
      var adv = new AdvWatcher.Adv
      {
        Time = DateTime.Parse(timeStr),
        Description = descNode.InnerText.Trim(),
        Url = descNode.Attributes["href"].Value
      };

      return adv;
    }
  }
}