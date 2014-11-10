using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Dmitriev.AdWatcher.Kufar;

namespace Dmitriev.AdWatcher
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      CreateDatabase();
      
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }

    private static void CreateDatabase()
    {
      var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      Debug.Assert(assemblyPath != null);
      var dbFileName = Path.Combine(assemblyPath, "kufar.db");

      if (!File.Exists(dbFileName))
      {
        AdvRepository.CreateDB(dbFileName);
      }
    }
  }
}
