using System;
using Microsoft.AspNetCore.Mvc;

namespace TobinTaxer
{
    public class TobinTaxerController : Controller
    {
    
        // POST TobinTaxer
        [HttpPost]
        [Route("Tax")]
        public long Post([FromBody]string value)
        {
            long result = Convert.ToInt64(value);

            result = (result / 100) * 99;

            return result;
        }

    }
}