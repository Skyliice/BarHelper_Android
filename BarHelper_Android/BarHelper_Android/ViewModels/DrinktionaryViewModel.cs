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
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            } 
        }
        public IAsyncCommand<Drink> TappedItem { get; protected set; }
        public ICommand SearchCommand { get; protected set; }
        public ICommand RefreshListCommand { get; protected set; }
        public INavigation Navigation;
        private IGatherable _gatherer;
        
        private List<Drink> _allDrinks { get; set; }
        private List<Drink> _drinks { get; set; }
        private bool _isRefreshing;
        public DrinktionaryViewModel()
        {
            TappedItem = new AsyncCommand<Drink>(DrinkClicked);
            SearchCommand = new Command(SearchDrinks);
            RefreshListCommand = new Command(RefreshDrinks);
            SearchString=String.Empty;
            _allDrinks = new List<Drink>();
            Drinks = new List<Drink>();
            var dsource = DataSource.getInstance();
            if (!dsource.CheckConnection())
            {
                DependencyService.Get<IMessage>().ShortAlert("Connection error, try again later");
                return;
            }
            dsource.SetValues();
            _allDrinks = dsource.GetDrinks();
            Drinks = _allDrinks;
        }
        
        private async Task DrinkClicked(Drink clickedDrink)
        {
            await Navigation.PushAsync(new DrinkDetailView(clickedDrink));
        }

        private void SearchDrinks()
        {
            if (SearchString == String.Empty)
                Drinks = _allDrinks;
            var temprecords =
                _allDrinks.Where(c => c.Name.ToLower().Contains(SearchString.ToLower()) | c.Description.ToLower().Contains(SearchString.ToLower())).ToList();
            Drinks = temprecords;
        }

        private void RefreshDrinks()
        {
            IsRefreshing = true;
            var dsource = DataSource.getInstance();
            if (!dsource.CheckConnection())
            {
                DependencyService.Get<IMessage>().ShortAlert("Connection error, try again later");
                IsRefreshing = false;
                return;
            }
            dsource.SetValues();
            _allDrinks = dsource.GetDrinks();
            Drinks = _allDrinks;
            IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}