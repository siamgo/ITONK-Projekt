using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Broker.Model;
using Broker.Business;

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
        public async Task<IActionResult> Buy([Bind("BuyerId,CustomerId,Name")] Buyer buyer)
        {
            using (StaticHttpClient client = new StaticHttpClient())
            {
                var record = client.PostAsync("https://itonk-grp2.firebaseio.com/Broker/Buyer", buyer);

                string stocks = await client.GetAsync("https://itonk-grp2.firebaseio.com/Broker/Seller");

                // post to Shareowner

                //await client.PostAsync() // Post to Buyer
            }
            return Ok();
        }
     
    }
}
