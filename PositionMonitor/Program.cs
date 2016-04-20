using System;
using System.Collections.Generic;

namespace PositionMonitor
{
   /// <summary>
   /// Generates an output to console containing your market position for the CSV file provided as arguments
   /// </summary>
   class Program
   {
      static void Main(string[] args)
      {
         
         List<string> columns;
         IList<Dictionary<string, string>> data;

         DateTime dateTime;
         if (DateTime.TryParse(args[1], out dateTime)) //handling entered date exceptions
            {
                Console.WriteLine("Date entered was: {0}", dateTime);
            }
            else
            {
                Console.WriteLine("Date entered was invalid. Please enter a correct date in dd/mm/yyyy format.");
                Console.ReadLine();
                return;
            }

         string dateStr = dateTime.ToString("dd/MM/yyyy");
         new TradeReader().GetData(args[0],dateStr, out columns, out data);

         var instrumentPositions = new Dictionary<string, decimal[]>();
         foreach (var row in data)
         {
             if (!instrumentPositions.ContainsKey(row["instrument name"])) //DRY; same line of code
                {
                    instrumentPositions.Add(row["instrument name"], new[] { 0.0M, 0.0M });
                }

             var qty = decimal.Parse(row["quantity"]); //DRY; same line of code

             switch (row["side"].ToUpper()) /* using switch construct instead of if/else make the code more readable;
                    only case-insensitive BUY and SELL are accepted otherwise an exception is being raised */
             {
                    case "BUY":
                        instrumentPositions[row["instrument name"]][0] += qty;
                        break;
                    case "SELL":
                        instrumentPositions[row["instrument name"]][1] += qty;
                        break;
                    default:
                        throw new NotSupportedException("Trade side was invalid!");
             }
         }

         var tradeString = TradeStringBuilder.BuildString(instrumentPositions);
         Console.WriteLine("Here is your current position in the market:");
         Console.WriteLine(tradeString);

         Console.ReadKey();
      }
   }
}
