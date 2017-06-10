using System;
using System.Collections.Generic;
using System.Net;
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
        [HttpPost]
        [Route("Sell")]
        //Localhost example http://localhost:49814/api/provider/sell?userId=11&stockId=10&amount=5322&price=233
        public async Task<IActionResult> Sell(string userId, int? stockId, uint? amount, int? price)
        {
            if (string.IsNullOrEmpty(userId) || stockId == null || amount == null || price == null)
            {
                return BadRequest("One of the required parameters were null");
            }

            if (amount == 0)
            {
                return BadRequest("Amount cannot be 0");
            }

            if (!CheckIfStocksAreAvailable(userId, stockId, amount).Result)
            {
                return BadRequest("Seller does not have the required stocks");
            }

            //Call other service
            try
            {
                var result = await SendTransaction(userId, stockId, amount, price);

                if (result)
                    return Ok();

                return StatusCode(500, "Failed calling broker service");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed calling broker service");
            }
        }

        private async Task<bool> SendTransaction(string userId, int? stockId, uint? amount, int? price)
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

            Console.WriteLine("Base url of broker service is " + baseUrl);

            const string endPoint = "/Sellers/Sell";

            var combinedUrl = "http://" + baseUrl + endPoint;

            try
            {
                using (var client = new HttpClient())
                {
                    var dictionary = new Dictionary<string, string>();

                    dictionary.Add(nameof(userId), userId);
                    dictionary.Add(nameof(stockId), stockId.ToString());
                    dictionary.Add(nameof(amount), amount.ToString());
                    dictionary.Add(nameof(price), price.ToString());

                    Console.WriteLine("Trying to call url " + combinedUrl);

                    var content = new FormUrlEncodedContent(dictionary);

                    var response = await client.PostAsync(combinedUrl, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
