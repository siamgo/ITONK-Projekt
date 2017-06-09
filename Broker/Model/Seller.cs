using System.Collections.Generic;

namespace Broker.Model
{
    public class Seller 
    {
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
