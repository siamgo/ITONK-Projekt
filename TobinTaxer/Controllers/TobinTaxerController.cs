using System;
using Microsoft.AspNetCore.Mvc;

namespace TobinTaxer
{
    [Route("[controller]")]
    public class TobinTaxerController : Controller
    {
        

        // POST TobinTaxer
        [HttpPost]
        public long Post([FromBody]string value)
        {
            long result = Convert.ToInt64(value);

            result = (result / 100) * 99;

            return result;
        }

    }
}