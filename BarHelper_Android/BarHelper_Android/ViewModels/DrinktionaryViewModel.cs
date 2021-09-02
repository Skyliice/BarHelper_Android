using System.Collections.Generic;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using BarHelper_Android.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BarHelper_Android.ViewModels
{
    public class DrinktionaryViewModel
    {
        public List<Drink> Drinks { get; set; }
        
        public IAsyncCommand<Drink> TappedItem { get; protected set; }
        private IGatherable _gatherer;
        public INavigation Navigation;

        private async Task DrinkClicked(Drink clickedDrink)
        {
            await Navigation.PushAsync(new DrinkDetailView(clickedDrink));
        }
        public DrinktionaryViewModel()
        {
            TappedItem = new AsyncCommand<Drink>(DrinkClicked);
            _gatherer = new ApiGatherer();
            Drinks = new List<Drink>();
            Drinks = Task.Run(() => _gatherer.GetAllDrinks()).Result;
        }
    }
}