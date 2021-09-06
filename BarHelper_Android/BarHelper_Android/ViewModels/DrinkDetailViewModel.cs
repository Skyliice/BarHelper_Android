using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BarHelper_Android.Models;
using BarHelper_Android.Views;
using Xamarin.Forms;

namespace BarHelper_Android.ViewModels
{
    public class DrinkDetailViewModel
    {
        public Drink CurrentDrink { get; set; }
        public INavigation Navigation;

        public List<Component> Recipe { get; set; }
        private IGatherable _gatherer;
        public ICommand BtnBackCommand { get; protected set; }
        public DrinkDetailViewModel(Drink chosenDrink)
        {
            CurrentDrink = chosenDrink;
            _gatherer = new ApiGatherer();
            var dsource = DataSource.getInstance();
            var lst = dsource.GetComponents();
            CurrentDrink.Recipe.ForEach(i=>i.Name=lst.Select(o=>o).First(x => x.ID==i.ID).Name);
            BtnBackCommand = new Command(BtnBackPressed);
        }

        private async void BtnBackPressed()
        {
            await Navigation.PopAsync();
        }
    }
}