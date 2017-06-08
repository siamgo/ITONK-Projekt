using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Broker.Model;
using Broker.Business;

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
        public async Task<IActionResult> Sell(Seller seller)
        {
            using (StaticHttpClient client = new StaticHttpClient())
            {
                var record = client.PostAsync("https://itonk-grp2.firebaseio.com/Broker/Seller", seller);
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
