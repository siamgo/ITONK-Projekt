

using System;
using System.Collections.Generic;
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

            var logEntry = new Dictionary<DateTime,string>();
            logEntry.Add(DateTime.Now,msg);
           
            using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(endpoint);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var message = JsonConvert.SerializeObject(logEntry);
                    
                    var content = new StringContent(message,Encoding.UTF8,"application/json");
                    var response = await client.PostAsync(endpoint,content);
                    Console.WriteLine(message);
                }
        }


    }

    public class LogEntry{
        public DateTime Date{get;set;}
        public string Message{get;set;}

    }

}