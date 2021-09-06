using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BarHelper_Android.Models;
using BarHelper_Android.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BarHelper_Android.ViewModels
{
    public class DrinktionaryViewModel : INotifyPropertyChanged
    {
        private List<Drink> _allDrinks { get; set; }
        private List<Drink> _drinks { get; set; }

        private string[] _localImages;

        public List<Drink> Drinks
        {
            get => _drinks;
            set
            {
                _drinks = value;
                OnPropertyChanged("Drinks");
            }
        }

        public string SearchString { get; set; }
        
        public IAsyncCommand<Drink> TappedItem { get; protected set; }
        public ICommand SearchCommand { get; protected set; }
        private IGatherable _gatherer;
        public INavigation Navigation;

        private async Task DrinkClicked(Drink clickedDrink)
        {
            await Navigation.PushAsync(new DrinkDetailView(clickedDrink));
        }
        public DrinktionaryViewModel()
        {
            /*_localImages = new string[] {"1.png", "2.png", "3.png", "4.png", "5.png", "6.png", "7.png", "8.png"};*/
            TappedItem = new AsyncCommand<Drink>(DrinkClicked);
            SearchString=String.Empty;
            SearchCommand = new Command(SearchDrinks);
            _gatherer = new ApiGatherer();
            _allDrinks = new List<Drink>();
            Drinks = new List<Drink>();
            _allDrinks = Task.Run(() => _gatherer.GetAllDrinks()).Result;
            /*var rnd = new Random();
            foreach (var drink in _allDrinks)
            {
                if (drink.Image == String.Empty)
                    drink.Image = _localImages[rnd.Next(0,_localImages.Length-1)];
            }*/
            Drinks = _allDrinks;
        }

        private void SearchDrinks()
        {
            if (SearchString == String.Empty)
                Drinks = _allDrinks;
            var temprecords =
                _allDrinks.Where(c => c.Name.ToLower().Contains(SearchString.ToLower()) | c.Description.ToLower().Contains(SearchString.ToLower())).ToList();
            Drinks = temprecords;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}