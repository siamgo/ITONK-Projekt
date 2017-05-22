

using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace FBLogger{
    public class FireBaseLogger{

        private string endpoint;
        public FireBaseLogger()
        {
            this.endpoint = "https://itonk-grp2.firebaseio.com/logging/publicshareowner.json";
        }

        public async void Log(string msg){

            
            var log = new LogEntry{
                Date = DateTime.Now,
                Message = msg
            };

            using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(endpoint);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var message = JsonConvert.SerializeObject(log);
                    Console.WriteLine(message);
                    var content = new StringContent(message.ToString(),Encoding.UTF8,"application/json");
                    var response = await client.PostAsync(endpoint,content);
                }
        }

    }

    public class LogEntry{
        public DateTime Date{get;set;}
        public string Message{get;set;}

    }

}