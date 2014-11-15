using System;
using System.IO;
using NUnit.Framework;

namespace KufarAdWatcher.Tests
{
  public class BaseTest
  {
    protected string _testDbFileName;

    [SetUp]
    public void SetUp()
    {
      Console.WriteLine("SetUp()");
      _testDbFileName = null;
    }

    [TearDown]
    public void TearDown()
    {
      Console.WriteLine("TearDown()");
      if (string.IsNullOrWhiteSpace(_testDbFileName))
      {
        return;
      }
      try
      {
        File.Delete(_testDbFileName);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Unable to delete DB file name {0}: {1}", _testDbFileName, ex.Message);
      }
    }
  }
}