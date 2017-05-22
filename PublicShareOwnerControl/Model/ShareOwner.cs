using System.Collections.Generic;
using PublicShareOwnerControl.Model;

namespace PublicShareOwnerControl.Model{
    public class ShareOwner{
        public int CustomerId {get;set;}
        public string Name  {get;set;}
        public List<Stock> Stocks{get;set;}

    }
}