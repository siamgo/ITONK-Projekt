using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.Model
{
    public class Buyer
    {
        public int BuyerId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Stock> WishList { get; set; }
    }
}
