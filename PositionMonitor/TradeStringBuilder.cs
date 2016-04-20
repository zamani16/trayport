using System;
using System.Collections.Generic;
using System.Linq;

namespace PositionMonitor
{
   /// <summary>
   /// Builds a pretty string which can output to the console for the given trade data
   /// </summary>
   public static class TradeStringBuilder
   {
      public static string BuildString(Dictionary<string, decimal[]> trades)
      {
         var prettyTrades = trades.Select(t =>
         {
            var instrument = t.Key;
            var bought = t.Value.First();
            var sold = t.Value.Last();

            return $"Instrument: {instrument,20}, Bought: {bought}, Sold: {sold}";
         });

         return string.Join(Environment.NewLine, prettyTrades);
      }
   }
}
