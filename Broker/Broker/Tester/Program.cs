using Broker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            Seller seller = new Seller()
            {
                Name = "Bob",
                CustomerId = 1,
                SellerId = 1,
                Stocks = new List<Stock>() { new Stock() { Name = "dc", Price = 10, StockId = 2, Amount = 1 } }
            };

            string json = JsonConvert.SerializeObject(seller);
        }
    }
}