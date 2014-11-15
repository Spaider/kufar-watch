using System;
using System.Data;
using System.Data.SQLite;
using BLToolkit.Data;
using BLToolkit.Data.Linq;

namespace Dmitriev.AdWatcher.DAL
{
  public class AdvDb : DbManager
  {
    public AdvDb() : base("SQLite")
    {
    }

    public AdvDb(IDbConnection connection) : base(connection)
    {
    }

    public static AdvDb DbFactory(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentNullException("fileName");

      var connStrBuilder = new SQLiteConnectionStringBuilder
      {
        DataSource = fileName
      };

      var conn = new SQLiteConnection(connStrBuilder.ConnectionString);
      return new AdvDb(conn);
    }

    public Table<AdvWatcher.Adv>      Advs      { get { return GetTable<AdvWatcher.Adv>();      }}
    public Table<AdvWatcher.Feed>     Feed      { get { return GetTable<AdvWatcher.Feed>();     }}
    public Table<AdvWatcher.Settings> Settings  { get { return GetTable<AdvWatcher.Settings>(); }}
  }
}