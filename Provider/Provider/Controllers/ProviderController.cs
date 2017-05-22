using System;
using Microsoft.AspNetCore.Mvc;

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
        public bool Sell(int? userId, int? stockId, int? amount, int? price)
        {
            if (userId == null || stockId == null || amount == null || price == null)
                return false;

            return new Random().Next(0, 2) == 1;
        }
    }
}
