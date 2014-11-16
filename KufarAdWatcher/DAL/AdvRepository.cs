using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using BLToolkit.Data.Linq;
using Dmitriev.AdWatcher.DAL;

namespace Dmitriev.AdWatcher.Kufar
{
  public class AdvRepository : IDisposable
  {
    private readonly AdvDb  _db;
    private readonly bool   _ownDb;

    public AdvRepository(AdvDb db)
    {
      if (db != null)
      {
        _db = db;
        _ownDb = false;
      }
      else
      {
        _db = new AdvDb();
        _ownDb = true;
      }
    }

    public AdvRepository(string fileName)
    {
      _db = AdvDb.DbFactory(fileName);
      _ownDb = true;
    }

    public void SaveAdvs(IEnumerable<AdvWatcher.Adv> advs)
    {
      _db.BeginTransaction();
      _db.InsertBatch(advs);
      _db.CommitTransaction();
    }

    public void MarkAllAsRead()
    {
      _db.Advs.Set(a => a.IsRead, true).Update();
    }

    public void MarkAsRead(IEnumerable<int> advIds)
    {
      _db.BeginTransaction();
      foreach (var id in advIds)
      {
        var id1 = id;
        _db.Advs
          .Where(a => a.Id == id1)
          .Set(a => a.IsRead, true)
          .Update();
      }
      _db.CommitTransaction();
    }

    public IEnumerable<AdvWatcher.Adv> GetAdvs()
    {
      return _db.Advs;
    } 

    public IEnumerable<AdvWatcher.Adv> GetLatestAdvs()
    {
     return 
       from adv in _db.Advs
       orderby adv.Time descending
       select adv;
    }

    public void DeleteOld(DateTime beforeDate)
    {
      _db.Advs.Delete(a => a.Time < beforeDate);
    }

    public static void CreateDB(string dbFileName)
    {
      const string CREATE_DB_QUERY =
        @"CREATE TABLE [Adv] (
            [Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
            [Time] TIMESTAMP  NULL,
            [Description] TEXT  NOT NULL,
            [Url] TEXT  NOT NULL,
            [IsRead] BOOLEAN  NOT NULL);

          CREATE TABLE [Feed] (
            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            [Caption] TEXT NOT NULL,
            [Url] TEXT NOT NULL,
            [LastAdTime] TIMESTAMP NULL,
            [LastCheckTime] TIMESTAMP NULL
          )";

      var connStringBuilder = new SQLiteConnectionStringBuilder
      {
        DataSource = dbFileName
      };
      using (var conn = new SQLiteConnection(connStringBuilder.ConnectionString))
      {
        conn.Open();
        using (var cmd = new SQLiteCommand(CREATE_DB_QUERY, conn))
        {
          cmd.ExecuteNonQuery();
        }
        conn.Close();
      }
    }

    public void Dispose()
    {
      if (_ownDb && _db != null)
      {
        _db.Dispose();
      }
    }
  }
}