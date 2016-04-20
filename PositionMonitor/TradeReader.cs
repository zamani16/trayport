using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PositionMonitor
{
   /// <summary>
   /// Reads data from a csv file containing trade information
   /// </summary>
   public class TradeReader
   {
      public void GetData(string path, string date, out List<string> columns, out IList<Dictionary<string, string>> data)
      {
         //var charArray = new[] { ',', '-', ';' };// no need to use this as fields are comma separated
         data = new List<Dictionary<string, string>>();
         columns = new List<string>();

          string[] lines=null;
          try //using try/catch to handle exceptions
          { 
                 lines = File.ReadAllLines(path);
          }
          catch (FileNotFoundException)
          {
             Console.WriteLine("Log file not found! Please make sure the correct path to the log file is specified.");
             Console.ReadLine();
             return;
          }
         
         var firstLine = lines[0];
         var stringArray = firstLine.Split(',');

         for (var x = 0; x <= stringArray.GetUpperBound(0); x++)
         {
            columns.Add(stringArray[x]);
         }

         foreach (var line in lines.Skip(1))
         {
            if (!line.Contains(date)) continue; //if the logged line is not related to the passed date ignore it 
            stringArray = line.Split(',');
            var dataRow = new Dictionary<string, string>();
          
                 for (var x = 0; x < columns.Count; x++)
                 {
                     dataRow.Add(columns[x], stringArray[x]);
                 }
             data.Add(dataRow);
         }
      }
   }
}