using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;
using HtmlAgilityPack;

namespace Dmitriev.AdWatcher.Kufar
{
  public class AdvRepository
  {
    private readonly string _url;

    public AdvRepository(string url)
    {
      _url = url;
    }

    public IEnumerable<AdvWatcher.Adv> GetAdvs()
    {
      var web = new HtmlWeb();
      var doc = web.Load(_url);
      var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"list_item_thumbs\"]/article");
      
      return nodes.Select(CreateAdFromNode);
    }

    public void SaveAdvs(IEnumerable<AdvWatcher.Adv> advs)
    {
      using (var db = new AdvDb())
      {
        db.BeginTransaction();
        db.InsertBatch(advs);
        db.CommitTransaction();
      }
    }

    public void MarkAsRead(IEnumerable<int> advIds)
    {
      using (var db = new AdvDb())
      {
        db.BeginTransaction();
        foreach (var id in advIds)
        {
          var id1 = id;
          db.Advs
            .Where(a => a.Id == id1)
            .Set(a => a.IsRead, true)
            .Update();
        }
        db.CommitTransaction();
      }
    }

    public IEnumerable<AdvWatcher.Adv> GetLatestAdvs()
    {
      using (var db = new AdvDb())
      {
        return 
          from adv in db.Advs
          orderby adv.Time descending
          select adv;
      }
    } 

    public static void CreateDB(string dbFileName)
    {
      const string CREATE_ADV_TABLE_QUERY =
        @"CREATE TABLE [Adv] (
          [Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
          [Time] TIMESTAMP  NULL,
          [Description] TEXT  NOT NULL,
          [Url] TEXT  NOT NULL,
          [IsRead] BOOLEAN  NOT NULL)";

      var connStringBuilder = new SQLiteConnectionStringBuilder
      {
        DataSource = dbFileName
      };
      using (var conn = new SQLiteConnection(connStringBuilder.ConnectionString))
      {
        conn.Open();
        using (var cmd = new SQLiteCommand(CREATE_ADV_TABLE_QUERY, conn))
        {
          cmd.ExecuteNonQuery();
        }
        conn.Close();
      }
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