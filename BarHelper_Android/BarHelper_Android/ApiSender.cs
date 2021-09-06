using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using Newtonsoft.Json;

namespace BarHelper_Android
{
    public class ApiSender : ISendable
    {

        private string url = "http://dogteam.ru/barhelper/";
        public async Task<bool> AddLiq(string name)
        {
            using (var client= new HttpClient())
            {
                var uri = new Uri(string.Concat(url, "addliq.php"));
                client.BaseAddress = uri;
                var parameters = new List<KeyValuePair<string, string>> { new("name",name) };
                var content = new FormUrlEncodedContent(parameters);
                var response = client.PostAsync(uri, content).Result;
                var str = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<ReceivedObject>(str);
                if (obj.Mess==2)
                    return true;
                else 
                    return false;
            }
        }

        public async Task AddDrink(string name, string description, string image)
        {
            throw new System.NotImplementedException();
        }
    }
}