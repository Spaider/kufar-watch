using BLToolkit.Data;
using BLToolkit.Data.Linq;

namespace Dmitriev.AdWatcher.DAL
{
  public class AdvDb : DbManager
  {
    public AdvDb() : base("SQLite")
    {
      
    }

    public Table<AdvWatcher.Adv> Advs { get { return GetTable<AdvWatcher.Adv>(); }}
  }
}