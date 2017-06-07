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
            try
            {
                SendTransaction(userId, stockId, amount, price);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed calling broker service " + e);
                return false;
            }
          
            return true;
        }

        private void SendTransaction(string userId, int? stockId, uint? amount, int? price)
        {
            Console.WriteLine("Trying to get environment variables");

            string hostName, portName;

            try
            {
                hostName = Environment.GetEnvironmentVariable("BROKER_SERVICE_HOST");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed getting environment variable BROKER_SERVICE_HOST");
                throw;
            }

            try
            {
                portName = Environment.GetEnvironmentVariable("BROKER_SERVICE_PORT");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed getting environment variable BROKER_SERVICE_PORT");
                throw;
            }

            if (hostName == null)
            {
                Console.WriteLine("BROKER_SERVICE_HOST environment variable was null");
                throw new NullReferenceException(nameof(hostName));
            }

            if (portName == null)
            {
                Console.WriteLine("BROKER_SERVICE_PORT environment variable was null");
                throw new NullReferenceException(nameof(portName));
            }

            var baseUrl = hostName + ":" + portName;
        
            Console.WriteLine("Base url is " + baseUrl);
            //Console.WriteLine("Trying to send request to Broker Service");

            //var endPoint = "/api/broker";

            //var combinedUrl = baseUrl + endPoint;

            //using (var client = new HttpClient())
            //{
            //    var response = await client.GetAsync(path);

            //    if (!response.IsSuccessStatusCode) return false;

            //    var responseJson = await response.Content.ReadAsStringAsync();

            //    if (string.IsNullOrWhiteSpace(responseJson) || responseJson == "null")
            //        return false;

            //    var stock = JsonConvert.DeserializeObject<Stock>(responseJson);

            //    return stock.Amount >= amount;
            //}

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
