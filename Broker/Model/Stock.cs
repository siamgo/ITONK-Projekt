using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.Model
{
    public class Stock
    {
        public int StockId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
