using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Broker.Model;
using Broker.Business;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Broker.Controllers
{
    [Route("[controller]")]
    public class SellersController : Controller
    {
      

        public SellersController()
        {
         
        }
        // POST: Sellers/Sell
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Sell")]
        public async Task<IActionResult> Sell([FromQuery]string userId, [FromQuery]int stockId,[FromQuery]uint amount, [FromQuery]int price)
        {
            int sellerId = new Random().Next(10000);
            var seller = new Seller(){
                SellerId = sellerId, Name = userId,Stocks = new List<Stock>(){new Stock{StockId = stockId,Amount = (int)amount, Price = price}}
            };

            using (StaticHttpClient client = new StaticHttpClient())
            {
                List<Seller> allSellers;
                var allSellsersJson = await client.GetAsync("https://itonk-grp2.firebaseio.com/Broker/Seller.json");
                if(allSellsersJson != "null"){
                    allSellers = JsonConvert.DeserializeObject<List<Seller>>(allSellsersJson);
                    allSellers.Add(seller);
                }else{
                    allSellers = new List<Seller>();
                    allSellers.Add(seller);
                }
                var record = await client.PutAsync("https://itonk-grp2.firebaseio.com/Broker/Seller.json", allSellers);
            }
            return Ok();
        }

        [Route("Index")]
        [HttpGet]
        public string Index()
        {
            return "Hello boy";
        }

    }
}
