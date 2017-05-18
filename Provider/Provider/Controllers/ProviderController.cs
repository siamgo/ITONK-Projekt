using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

namespace Provider.Controllers
{
    [Produces("application/json")]
    [Route("api/Provider")]
    public class ProviderController : Controller
    {
        //// GET: api/Provider
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Provider/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/Provider
        [HttpPost]
        public void Post([FromBody]string value)
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
        [Route("Buy")]
        //Localhost example http://localhost:49814/api/provider/buy?id=11&price=10&amount=5322
        public bool Buy(int id, int amount, int price)
        {           
            return new Random().Next(0, 2) == 1;
        }
    }
}
