using System;
using System.Globalization;

namespace Dmitriev.AdWatcher
{
  public static class Toolbox
  {
    public static string FormatPrice(float price, string currency)
    {
      if (string.IsNullOrWhiteSpace(currency))
      {
        throw new ArgumentNullException("currency");
      }

      string currStr;
      switch (currency.ToLowerInvariant())
      {
        case "byr":
          currStr = " руб.";
          break;

        case "usd":
          currStr = "$";
          break;

        case "eur":
          currStr = "€";
          break;
          
        default:
          currStr = currency;
          break;
      }

      return String.Format(CultureInfo.CurrentCulture, "{0:N0}{1}", price, currStr);
    }
  }
}