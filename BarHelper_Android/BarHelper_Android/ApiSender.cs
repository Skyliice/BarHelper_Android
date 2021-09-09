using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using Microsoft.Win32.SafeHandles;
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

        public async Task<bool> AddDrink(Drink newDrink,bool imageFromInternet)
        {
            using (var client = new HttpClient())
            {
                if (!imageFromInternet)
                {
                    var uri = new Uri(string.Concat(url, "cockImage.php"));
                    using (var data = new MultipartFormDataContent())
                    {
                        data.Headers.ContentType.MediaType = "multipart/form-data";
                        Stream fileStream = System.IO.File.OpenRead(newDrink.Image);
                        var filename = string.Format(@"{0}.jpg", System.Guid.NewGuid());
                        data.Add(new StreamContent(fileStream),"file",filename);
                        var msg = client.PostAsync(uri, data).Result;
                        var rsp = msg.Content.ReadAsStringAsync().Result;
                        var messobj = JsonConvert.DeserializeObject<ReceivedObject>(rsp);
                        if (messobj.Mess == 2)
                        {
                            newDrink.Image = url+messobj.Link;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                var page = new Uri(string.Concat(url, "adddrink.php"));
                var str = string.Empty;
                foreach (var liq in newDrink.Recipe)
                {
                    str += $"{liq.ID}:{liq.Amount};";
                }
                var parameters = new List<KeyValuePair<string, string>>
                {
                    new("name", newDrink.Name),
                    new("desc", newDrink.Description),
                    new("recipe", str),
                    new("img", newDrink.Image)
                };
                var content = new FormUrlEncodedContent(parameters);
                var response = client.PostAsync(page, content).Result;
                str = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<ReceivedObject>(str);
                if (obj.Mess==2)
                    return true;
                else 
                    return false;
            }
        }
    }
}