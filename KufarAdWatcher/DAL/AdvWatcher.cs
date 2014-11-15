﻿using System;
using BLToolkit.DataAccess;

namespace Dmitriev.AdWatcher.DAL
{
  public class AdvWatcher
  {
    public class Adv
    {
      [PrimaryKey, Identity]
      public int      Id          { get; set; }
      public DateTime Time        { get; set; }
      public string   Description { get; set; }
      public string   Url         { get; set; }
      public bool     IsRead      { get; set; }
    }

    public class Feed
    {
      [PrimaryKey, Identity]
      public int        Id            { get; set; }
      public string     Caption       { get; set; }
      public string     Url           { get; set; }
      public DateTime?  LastAdTime    { get; set; }
      public DateTime?  LastCheckTime { get; set; }
    }

    public class Settings
    {
      public DateTime LastCheckTime { get; set; }
    }
  }
}