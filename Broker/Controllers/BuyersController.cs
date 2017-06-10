using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Broker.Model;
using Broker.Business;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Broker.Controllers
{
    [Route("[controller]")]
    public class BuyersController : Controller
    {
    

        public BuyersController( )
        {
         
        }

     
        // POST: Buyers/Buy
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Buy")]
        public async Task<IActionResult> Buy([FromBody] ShareOwner buyer)
        {

            List<Seller> newSellers = new List<Seller>(){
                new Seller(){
                    Name = "Peter",
                    SellerId = 1001,
                    CustomerId = 2001,
                    Stocks = new List<Stock>(){
                        
                        new Stock(){
                            StockId = 11,
                            Name = "Shit",
                            Price = 10.0,
                            Amount = 10
                        }
                    }
                }
            };

            using (StaticHttpClient client = new StaticHttpClient())
            {
                var host = Environment.GetEnvironmentVariable("PSOC_SERVICE_HOST");
                var port = Environment.GetEnvironmentVariable("PSOC_SERVICE_PORT");
                var newSellersJson = JsonConvert.SerializeObject(newSellers);

                await client.PutAsync("https://itonk-grp2.firebaseio.com/Broker/Seller.json",newSellers);
                List<Buyer> buyers = new List<Buyer>(){new Buyer(){BuyerId = 0, Name = buyer.Name,CustomerId = buyer.CustomerId, Stocks =buyer.Stocks}};
                var record = await client.PutAsync("https://itonk-grp2.firebaseio.com/Broker/Buyer.json", buyers);
    
                string stocks = await client.GetAsync("https://itonk-grp2.firebaseio.com/Broker/Seller.json");

                var sellers = JsonConvert.DeserializeObject<List<Seller>>(stocks);

                ShareOwner newOwner = null;
                int wantedAmount = 0;
                Stock stockToRemove = null;
                Seller soldBy = null;
                foreach(var seller in sellers){
                    foreach (var stock in seller.Stocks)
                    {
                        foreach (var requestedStock in buyer.Stocks)
                        {   //You can only buy 1 stock at a time - will be set multiple times, but who cares really?
                            wantedAmount = requestedStock.Amount;
                            if(stock.Name == requestedStock.Name){
                                newOwner = new ShareOwner(){
                                    Name = buyer.Name,
                                    CustomerId = buyer.CustomerId,
                                    Stocks = new List<Stock>(){
                                        stock
                                    }
                                };
                                soldBy = seller;
                                stockToRemove = stock;
                                break;
                            }
                        }
                    }
                    
                }
                
                if(soldBy != null && soldBy.Stocks != null && stockToRemove != null)
                {
                    soldBy.Stocks.Remove(stockToRemove);
                }
                
                await client.PutAsync("https://itonk-grp2.firebaseio.com/Broker/Seller.json",sellers);
                
                // post to Shareowner
                if(newOwner != null){
                    //await client.PostAsync(host + port + "/api/shareowner",newOwner);
                }else{
                    return BadRequest();
                }
                //await client.PostAsync() // Post to Buyer
            }
            return Ok();
        }
     
    }
}
