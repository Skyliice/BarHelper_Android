using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BarHelper_Android.Models;
using BarHelper_Android.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Component = BarHelper_Android.Models.Component;

namespace BarHelper_Android.ViewModels
{
    public class SearchResultViewModel : INotifyPropertyChanged
    {
        public bool IsNoContent
        {
            get => isNoContent;
            set
            {
                isNoContent = value;
                OnPropertyChanged("IsNoContent");
            }
        }
        public ObservableCollection<Drink> Drinks
        {
            get => _drinks;
            set
            {
                _drinks = value;
                OnPropertyChanged("Drinks");
            }
        }
        public string SearchString { get; set; }

        private ObservableCollection<Component> _selectedComponents { get; set; }
        private ObservableCollection<Drink> _allDrinks;
        private ObservableCollection<Drink> _drinks;
        private bool isNoContent;
        private int _componentsquan;
        private IGatherable _gatherer;
        
        public INavigation Navigation;
        public ICommand SearchCommand { get; protected set; }
        public IAsyncCommand<Drink> TappedItem { get; protected set; }
        public ICommand BtnBackCommand { get; protected set; }
        public SearchResultViewModel(ObservableCollection<Component> selectedComponents, int componentsquan)
        {
            _selectedComponents = selectedComponents;
            _componentsquan = componentsquan;
            SearchCommand = new Command(SearchDrinks);
            TappedItem = new AsyncCommand<Drink>(DrinkClicked);
            BtnBackCommand = new Command(BtnBackPressed);
            SearchString=String.Empty;
            var dsource = DataSource.getInstance();
            var temprecords = dsource.GetDrinks();
            _allDrinks = new ObservableCollection<Drink>(temprecords);
            SelectAllAvailableRecipes();
            if (Drinks.Count == 0)
                IsNoContent = true;
            else
                IsNoContent = false;
        }

        private void SearchDrinks()
        {
            if (SearchString == String.Empty)
                Drinks = _allDrinks;
            var temprecords =
                _allDrinks.Where(c => c.Name.ToLower().Contains(SearchString.ToLower()) | c.Description.ToLower().Contains(SearchString.ToLower()));
            Drinks = new ObservableCollection<Drink>(temprecords);
        }

        private async Task DrinkClicked(Drink clickedDrink)
        {
            await Navigation.PushAsync(new DrinkDetailView(clickedDrink));
        }
        private async void BtnBackPressed()
        {
            await Navigation.PopAsync();
        }
        private void SelectAllAvailableRecipes()
        {
            Drinks = new ObservableCollection<Drink>();
            foreach (var drink in _allDrinks)
            {
                if(_selectedComponents.All(o=>drink.Recipe.Any(z=>z.ID==o.ID)) && drink.Recipe.Count<=_componentsquan)
                    Drinks.Add(drink);
            }
            _allDrinks = Drinks;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}