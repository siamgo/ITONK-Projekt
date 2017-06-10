using System;
using Microsoft.AspNetCore.Mvc;
using Requester.Model;

namespace Requester
{
    public class RequesterController : Controller
    {
    
        // POST /api/Request
        [HttpPost]
        [Route("Request")]
        public bool Post([FromQuery] string stockname,[FromQuery]int amount){
            
            var shareowner = new ShareOwner(){
                Name = ""
            };

            return true;
        }

    }
}