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
using String = System.String;

namespace BarHelper_Android.ViewModels
{
    public class AddDrinkRecipeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Liq> Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                OnPropertyChanged("Recipe");
            }
        }
        public Drink NewDrink { get; set; }
        public ObservableCollection<Component> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged("Components");
            }
        }
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged("SearchString");
            }
        }
        
        public Liq SelectedComponent
        {
            get => _selectecComponent;
            set
            {
                _selectecComponent = value;
                OnPropertyChanged("SelectedComponent");
            } 
        }

        private ObservableCollection<Component> _allComponents;
        private ObservableCollection<Component> _components;
        private ObservableCollection<Liq> _recipe;
        private Liq _selectecComponent;
        private bool _isInternetSourceImage;
        private string _searchString;

        public INavigation Navigation;
        public ICommand SearchCommand { get; protected set; }
        public IAsyncCommand<Component> ComponentTapped { get; protected set; }
        public IAsyncCommand<Liq> SelectedComponentTapped { get; protected set; }
        public ICommand AmountChangedCommand { get; protected set; }
        public ICommand NextCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public ICommand RemoveCommand { get; protected set; }
        
        public AddDrinkRecipeViewModel(Drink newDrink, bool isInternetSourceImage)
        {
            NewDrink = newDrink;
            _isInternetSourceImage = isInternetSourceImage;
            SearchCommand = new Command(SearchComponents);
            NextCommand = new Command(GoNext);
            BackCommand = new Command(GoBack);
            ComponentTapped = new AsyncCommand<Component>(TappedComponent);
            SelectedComponentTapped = new AsyncCommand<Liq>(TappedSelectedComponent);
            RemoveCommand = new Command(RemoveComponent);
            AmountChangedCommand = new Command(ChangeAmount);
            _allComponents = new ObservableCollection<Component>();
            Recipe = new ObservableCollection<Liq>();
            var dsource = DataSource.getInstance().GetComponents();
            foreach (var component in dsource)
            {
                _allComponents.Add(component);
            }
            Components = new ObservableCollection<Component>(_allComponents.OrderBy(o=>o.Name));
        }

        private void SearchComponents()
        {
            if (SearchString == String.Empty)
                Components = _allComponents;
            var temprecords = Components.Where(o => o.Name.ToLower().Contains(SearchString.ToLower()));
            var tempcollection = new ObservableCollection<Component>(temprecords.OrderBy(o=>o.Name));
            Components = tempcollection;
        }

        private async void GoNext()
        {
            if (Recipe.Count == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Pick at least one ingredient!");
                return;
            }
            NewDrink.Recipe = Recipe.ToList();
            await Navigation.PushAsync(new SendingDrinkView(NewDrink, _isInternetSourceImage));
        }

        private async void GoBack()
        {
            await Navigation.PopAsync();
        }
        
        private async Task TappedComponent(Component selectedComponent)
        {
            Recipe.Add(new Liq(){Name = selectedComponent.Name,ID = selectedComponent.ID,Amount = "0"});
            _allComponents.Remove(selectedComponent);
            Components.Remove(selectedComponent);
        }

        private async Task TappedSelectedComponent(Liq selectedComponent)
        {
            SelectedComponent = selectedComponent;
        }

        private void RemoveComponent()
        {
            if(SelectedComponent is null)
                return;
            Recipe.Remove(SelectedComponent);
            _allComponents.Add(new Component(){ID = SelectedComponent.ID, Name = SelectedComponent.Name});
            var tmp = _allComponents.OrderBy(o => o.Name);
            _allComponents = new ObservableCollection<Component>(tmp);
            Components = _allComponents;
            SelectedComponent = null;
        }

        private void ChangeAmount()
        {  
            if(SelectedComponent is null)
                return;
            Recipe.First(o => o.ID == SelectedComponent.ID).Amount = SelectedComponent.Amount;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}