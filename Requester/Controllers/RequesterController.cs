using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Requester.Model;
using System.Linq;

namespace Requester
{
    public class RequesterController : Controller
    {

        // POST /api/Request
        [HttpPost]
        [Route("Request")]
        public async Task<bool> PostAsync([FromQuery] string stockname, [FromQuery]int amount, [FromQuery]int customerid)
        {

            //Could check customerID in some sort of DB to get more info

            var shareowner = new ShareOwner()
            {
                Name = "Requester",
                CustomerId = customerid,
                Stocks = new List<Stock>() {
                    new Stock(){
                        StockId = getStockId(stockname),
                        Name = stockname,
                        Amount = amount,
                        Price = 0

                }}
            };

            if(await buy(shareowner))
            {
                await payTobinTax(shareowner);
                return true;    
            }

            return false;
        }

        private int getStockId(string stockname){
            Random rnd = new Random();
            int id = rnd.Next(10000);
            return id;
        }

        private async Task<ShareOwner> payTobinTax(ShareOwner shareowner){
            
            using(var client = new HttpClient()){
                var host = Environment.GetEnvironmentVariable("TOBINTAXER_SERVICE_HOST");
                var port = Environment.GetEnvironmentVariable("TOBINTAXER_SERVICE_PORT");

                var endpoint = new Uri("http://"+host+":"+port);
                var response = await client.PostAsync(endpoint+"TobinTaxer/Tax",new StringContent(shareowner.Stocks.FirstOrDefault().Price.ToString()));
                if(response.IsSuccessStatusCode){
                    var newPrice = Double.Parse(await response.Content.ReadAsStringAsync()); 
                    
                    shareowner.Stocks.FirstOrDefault().Price = newPrice;
                    return shareowner;
                }
                return null;
            }
        }

        private async Task<bool> buy(ShareOwner shareowner){
            if(shareowner != null)
                {
                    using (var client = new HttpClient())
                    {
                       
                       var host = Environment.GetEnvironmentVariable("BROKER_SERVICE_HOST");
                       var port = Environment.GetEnvironmentVariable("BROKER_SERVICE_PORT");

                       var endpoint = new Uri("http://"+host+":"+port);


                        //get all shareowners
                        var jsonShareOwner = JsonConvert.SerializeObject(shareowner);
                        var content = new StringContent(jsonShareOwner, Encoding.UTF8, "application/json");
                        var allOwners = await client.PostAsync(endpoint + "Buyers/Buy",content);

                        if(allOwners.IsSuccessStatusCode){
                            return true;
                        }
                    }
                }
                return false;
        }

    }


}