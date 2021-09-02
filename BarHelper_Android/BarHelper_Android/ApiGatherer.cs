using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using Newtonsoft.Json;

namespace BarHelper_Android
{
    public class ApiGatherer : IGatherable
    {
        public async Task<List<Drink>> GetAllDrinks()
        {
            var client = new HttpClient();
            string resultjson = await client.GetStringAsync("http://dogteam.ru/barhelper/showdrink.php");
            var resultList = new List<Drink>();
            resultList = JsonConvert.DeserializeObject<List<Drink>>(resultjson);
            return resultList;
        }

        public async Task<List<Component>> GetAllComponents()
        {
            var client = new HttpClient();
            string resultjson = await client.GetStringAsync("http://dogteam.ru/barhelper/showliq.php");
            var resultList = new List<Component>();
            resultList = JsonConvert.DeserializeObject<List<Component>>(resultjson);
            return resultList;
        }
    }
}