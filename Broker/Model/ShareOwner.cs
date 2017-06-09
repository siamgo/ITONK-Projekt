using System.Collections.Generic;
using Broker.Model;

namespace Broker.Model{
    public class ShareOwner{
        public int CustomerId {get;set;}
        public string Name  {get;set;}
        public List<Stock> Stocks{get;set;}

    }
}