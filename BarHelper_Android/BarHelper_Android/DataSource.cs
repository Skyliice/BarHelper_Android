using System.Collections.Generic;
using System.Threading.Tasks;
using BarHelper_Android.Models;

namespace BarHelper_Android
{
    public class DataSource
    {
        private static DataSource instance;
        private List<Component> Components;
        private List<Drink> Drinks;
        private IGatherable _gatherer;
        private ISendable _sender;

        private DataSource()
        {
            Drinks = new List<Drink>();
            _gatherer = new ApiGatherer();
            _sender = new ApiSender();
            Components = new List<Component>();
        }

        public void SetValues()
        {
            instance.Components = Task.Run(() => _gatherer.GetAllComponents()).Result;
            instance.Drinks = Task.Run(() => _gatherer.GetAllDrinks()).Result;
        }

        public List<Drink> GetDrinks()
        {
            return instance.Drinks;
        }

        public bool AddLiq(string name)
        {
            return _sender.AddLiq(name).Result;
        }

        public bool AddDrink(Drink newDrink,bool isImagefromInternet)
        {
            return _sender.AddDrink(newDrink,isImagefromInternet).Result;
        }

        public List<Component> GetComponents()
        {
            return instance.Components;
        }

        public static DataSource getInstance()
        {
            if (instance == null)
                instance = new DataSource();
            return instance;
        }
    }
}