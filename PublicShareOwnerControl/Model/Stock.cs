/*
Stock:
StockId: int
Name: string
Price: double
Amount/Quantity: int 
*/

namespace PublicShareOwnerControl.Model{

    public class Stock{

        public string Name{get;set;}
        public int StockId {get;set;}
        public double Price {get;set;}
        public int Amount {get;set;}

    }
}