using System;
using System.Collections.Generic;
using NUnit.Framework;
using PositionMonitor;

namespace PositionMonitorTests
{
   [TestFixture]
   public class TradeReaderTests
   {
      [Test]
      public void TestTradeReader()
      {
         var reader = new TradeReader();

         IList<Dictionary<string, string>> data;
         List<string> columns;
         reader.GetData(AppDomain.CurrentDomain.BaseDirectory + "\\trades.csv","22/05/2016", out columns, out data);

         Assert.IsFalse(data == null); //This is a better Assert
         Assert.IsTrue(columns.Count == 5); // Assert was incorrect
        
      }

   }
}
