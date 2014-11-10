using System;

namespace Dmitriev.AdWatcher.Kufar
{
  public class Adv
  {
    public DateTime Time        { get; set; }
    public string   Description { get; set; }
    public string   Url         { get; set; }
    public bool     IsRead      { get; set; }
  }
}