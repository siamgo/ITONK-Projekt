using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicShareOwnerControl;

namespace ShareOwnerController
{
     [Route("api/[controller]")]

     public class ShareOwnerController : Controller
     {
         // GET: api/shareowners/
         [HttpGet]
         public async Task<IDictionary<string,ShareOwner>> Get()
         {
            using (var client = new HttpClient())
            {
                var baseUri = "https://itonk-grp2.firebaseio.com/shareowners.json";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var owners = JsonConvert.DeserializeObject<IDictionary<string,ShareOwner>>(responseJson);

                    return owners;
                }

                return null;
            }
         }
         
         // GET: api/shareowners/search?name=something
         [HttpGet("Search")]
         public async Task<ShareOwner> Search(string name)
         {
              using (var client = new HttpClient())
            {
                var baseUri = "https://itonk-grp2.firebaseio.com/shareowners.json";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var owners = JsonConvert.DeserializeObject<IDictionary<string,ShareOwner>>(responseJson);

                   foreach (var shareowner in owners)
                   {
                       Console.WriteLine(shareowner.Value.Name);
                        if(shareowner.Value.Name.ToLower()== name)
                        {
                            return shareowner.Value;
                        }
                   }
                }
                return null;
             } 
         }

         [HttpPost]
         public async void Post([FromBody]string shareowner)
         {
           
                using (var client = new HttpClient())
                {
                    var baseUri = "https://itonk-grp2.firebaseio.com/shareowners.json";
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var owner = new ShareOwner{
                        Name = "Simon",
                        ShareHolderId = 1,
                        Shares = new List<Share>{
                            new Share{
                                Name = "ITONK",
                                ShareId = 1
                            }
                        }
                    };

                    var jsonShare = JsonConvert.SerializeObject(owner);

                    var content = new StringContent(jsonShare.ToString(),Encoding.UTF8,"application/json");
                    var response = await client.PostAsync(baseUri,content);
                }
            

         }
     }


}