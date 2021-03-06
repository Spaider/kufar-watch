﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dmitriev.AdWatcher.DAL;
using HtmlAgilityPack;

namespace Dmitriev.AdWatcher.Feeds
{
  public class Kufar
  {
    private readonly string _url;

    public Kufar(string url)
    {
      if (String.IsNullOrWhiteSpace(url))
        throw new ArgumentNullException("url");

      _url = url;
    }

    public IEnumerable<AdvWatcher.Adv> GetAdvs()
    {
      try
      {
        var web = new HtmlWeb();
        var doc = web.Load(_url);
        var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"list_item_thumbs\"]/article");

        return nodes != null ? nodes.Select(CreateAdFromNode) : new AdvWatcher.Adv[0];
      }
      catch (Exception e)
      {
        Trace.Fail("Ошибка загрузки ленты: " + e.Message);
        Trace.Fail(e.StackTrace);
        throw;
      }
    }

    private static AdvWatcher.Adv CreateAdFromNode(HtmlNode node)
    {
      var timeNode = node.SelectSingleNode("time");
      var timeStr = timeNode.Attributes["datetime"].Value;

      var descNode = node.SelectSingleNode("div[2]/a[1]");
      var priceInfoNode = node.SelectSingleNode("div[2]/b");

      double price = 0;
      string currency = null;
      
      if (priceInfoNode != null)
      {
        var priceNode = priceInfoNode.SelectSingleNode("meta[@itemprop=\"price\"]");
        var currencyNode = priceInfoNode.SelectSingleNode("meta[@itemprop=\"priceCurrency\"]");

        if (priceNode != null && currencyNode != null)
        {
          var priceAttr = priceNode.Attributes["content"];
          var currencyAttr = currencyNode.Attributes["content"];

          if (priceAttr != null && currencyAttr != null)
          {
            double.TryParse(priceAttr.Value, out price);
            currency = currencyAttr.Value;
          }
        }
      }

      // Kufar stores has local time on page but shows them as UTC
      var adv = new AdvWatcher.Adv
      {
        Time = DateTime.Parse(timeStr).ToUniversalTime(),
        Description = descNode.InnerText.Trim(),
        Url = descNode.Attributes["href"].Value,
        Price = Math.Abs(price) < 1E-3 ? null : (double?) price,
        Currency = currency
      };

      return adv;
    }
  }
}