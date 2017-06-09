using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicShareOwnerControl.Model;
using FBLogger;

namespace ShareOwnerController
{
     [Route("api/[controller]")]

     public class ShareOwnerController : Controller
     {
        private FireBaseLogger _fbLogger;
        private string _guid;
        private string TAG = "PUBLICSHAREOWNER: ";
         public ShareOwnerController()
         {
            this._fbLogger = new FireBaseLogger();
         }
         // GET: api/shareowners/
         [HttpGet]
         public async Task<List<ShareOwner>> Get()
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
                    try 
                    {
                        
                        var owners = JsonConvert.DeserializeObject<List<ShareOwner>>(responseJson);
                        this._fbLogger.Log(TAG + "SUCCESS in GET: api/shareowner");
                        return owners;
                    }catch(JsonSerializationException e){
                        return null;
                    }
                  
                }
                this._fbLogger.Log(TAG + "ERROR in GET: api/shareowner");
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
                    this._fbLogger.Log(TAG + "SUCCESS in // GET: api/shareowners/search?name="+name);
                   foreach (var shareowner in owners)
                   {
                       Console.WriteLine(shareowner.Value.Name);
                        if(shareowner.Value.Name.ToLower()== name)
                        {
                            return shareowner.Value;
                        }
                   }
                }
                this._fbLogger.Log(TAG + "FAILURE in // GET: api/shareowners/search?name="+name + " - no match");
                return null;
             } 
         }

         [HttpPost]
         public async void Post([FromBody]ShareOwner shareowner)
         {

                           
                if(shareowner != null)
                {
                    using (var client = new HttpClient())
                    {
                        var baseUri = "https://itonk-grp2.firebaseio.com/shareowners.json";
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Accept.Clear();

                        //get all shareowners
                        var allOwners = await client.GetAsync(baseUri);
                        if(allOwners.IsSuccessStatusCode){
                            //var temp = JsonConvert.DeserializeObject<Dictionary<string,List<ShareOwner>>>(await allOwners.Content.ReadAsStringAsync());
                            string ownercontent = await allOwners.Content.ReadAsStringAsync();
                            var owners = JsonConvert.DeserializeObject<List<ShareOwner>>(ownercontent);
                            owners.Add(shareowner);
                            var jsonShare = JsonConvert.SerializeObject(owners);
                            Console.WriteLine(jsonShare);
                            var content = new StringContent(jsonShare.ToString(),Encoding.UTF8,"application/json");
                            var response = await client.PutAsync(baseUri,content);
                            
                            this._fbLogger.Log(TAG+"SUCCESS in POST: api/shareowner");
                        }
                    }
                }else{
                    this._fbLogger.Log(TAG + "FAILURE in POST: api/shareowner" );
                }
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> Update(long id, [FromBody]ShareOwner shareowner){
            
            
            if(shareowner == null){
                return BadRequest();
            }

            var owners = await Get();
            ShareOwner toUpdate;
            foreach (var owner in owners)
            {
                /*if(owner.Value.CustomerId == id)
                {
                    toUpdate = owner.Value;
                }else{
                    return BadRequest();
                }*/
            }
            

            

            using (var client = new HttpClient())
            {
                var baseUri = "https://itonk-grp2.firebaseio.com/shareowners.json/";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                var jsonShare = JsonConvert.SerializeObject(shareowner);

                var content = new StringContent(jsonShare.ToString(),Encoding.UTF8,"application/json");
                var response = await client.PutAsync(baseUri,content);
            }

            return new NoContentResult();
         }


     }


}