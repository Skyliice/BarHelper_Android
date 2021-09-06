using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using Newtonsoft.Json;

namespace BarHelper_Android
{
    public class ApiGatherer : IGatherable
    {
        private readonly string url = "http://dogteam.ru/barhelper/";
        public async Task<List<Drink>> GetAllDrinks()
        {
            var client = new HttpClient();
            string resultjson = await client.GetStringAsync(url+"showdrink.php");
            var resultList = new List<Drink>();
            resultList = JsonConvert.DeserializeObject<List<Drink>>(resultjson);
            return resultList;
        }

        public async Task<List<Component>> GetAllComponents()
        {
            var client = new HttpClient();
            string resultjson = await client.GetStringAsync(url+"showliq.php");
            var resultList = new List<Component>();
            resultList = JsonConvert.DeserializeObject<List<Component>>(resultjson);
            return resultList;
        }
    }
}