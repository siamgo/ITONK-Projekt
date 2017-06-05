using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Provider.Models;

namespace Provider.Controllers
{
    [Produces("application/json")]
    [Route("api/Provider")]
    public class ProviderController : Controller
    {        
        // POST: api/Provider
        [HttpPost]
        [Route("Add")]
        public void Post(int userId, int stockId, int amount)
        {
                
        }
        
        // PUT: api/Provider/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("Sell")]
        //Localhost example http://localhost:49814/api/provider/sell?userId=11&stockId=10&amount=5322&price=233
        public bool Sell(string userId, int? stockId, uint? amount, int? price)
        {
            if (string.IsNullOrEmpty(userId) || stockId == null || amount == null || price == null)
                return false;

            if (amount == 0)
                return false;

            if(!CheckIfStocksAreAvailable(userId, stockId, amount).Result)
                return false;

            //Call other service    
            //SendTransaction(userId, stockId, amount, price);

            return true;
        }

        private void SendTransaction(string userId, int? stockId, uint? amount, int? price)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CheckIfStocksAreAvailable(string userId, int? stockId, uint? amount)
        {
            using (var client = new HttpClient())
            {
                var path = "https://itonk-grp2.firebaseio.com/";

                path += "users/";
                path += userId + "/";
                path += "stocks/";
                path += stockId;
                path += ".json";
               
                var response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode) return false;

                var responseJson = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseJson) || responseJson == "null")
                    return false;

                var stock = JsonConvert.DeserializeObject<Stock>(responseJson);

                return stock.Amount >= amount;
            }
        }
    }
}
