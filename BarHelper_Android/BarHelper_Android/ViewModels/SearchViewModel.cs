using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BarHelper_Android.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Component = BarHelper_Android.Models.Component;

namespace BarHelper_Android.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Component> _allComponents { get; set; }
        private ObservableCollection<Component> _components { get; set; }

        public ObservableCollection<Component> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged("Components");
            }
        }

        private ObservableCollection<Component> _selectedComponents { get; set; }
        public ObservableCollection<Component> SelectedComponents
        {
            get => _selectedComponents;
            set
            {
                _selectedComponents = value;
                OnPropertyChanged("SelectedComponents");
            } 
        }
        public IAsyncCommand<Component> ComponentTapped { get; protected set; }
        public ICommand ResetCommand { get; protected set; }
        public IAsyncCommand MixCommand { get; protected set; }
        public INavigation Navigation;
        public IAsyncCommand<Component> RemoveComponentTapped { get; protected set; }

        private int _sliderValue;
        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                if (_sliderValue < 1)
                    _sliderValue = 1;
                OnPropertyChanged("SliderValue");
            } }
        public ICommand SearchCommand { get; protected set; }
        private IGatherable _gatherable;
        public string SearchString { get; set; }

        public SearchViewModel()
        {
            ComponentTapped = new AsyncCommand<Component>(TappedComponent);
            MixCommand = new AsyncCommand(MixButtonPressed);
            ResetCommand = new Command(ResetRecipe);
            SliderValue = 1;
            RemoveComponentTapped = new AsyncCommand<Component>(RemoveComponent);
            SearchCommand = new Command(CommandSearch);
            _allComponents = new ObservableCollection<Component>();
            var dsource = DataSource.getInstance();
            var templst = dsource.GetComponents().OrderBy(o=>o.Name);
            foreach (var component in templst)
            {
                _allComponents.Add(component);
            }
            SelectedComponents = new ObservableCollection<Component>();
            Components = _allComponents;
        }

        private async Task MixButtonPressed()
        {
            if (SelectedComponents.Count == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Pick at least 1 component!");
                return;
            }
            await Navigation.PushAsync(new SearchResultView(SelectedComponents,SliderValue));
        }

        private void ResetRecipe()
        {
            foreach (var component in SelectedComponents)
            {
                Components.Add(component);
            }
            SelectedComponents.Clear();
            var temprecords = Components.OrderBy(o => o.Name);
            Components = new ObservableCollection<Component>(temprecords);
            _allComponents = new ObservableCollection<Component>(temprecords);
        }

        private void CommandSearch()
        {
            if (SearchString == String.Empty)
                Components = _allComponents;
            var temprecords = Components.Where(o => o.Name.ToLower().Contains(SearchString.ToLower()));
            var tempcollection = new ObservableCollection<Component>(temprecords);
            Components = tempcollection;
        }

        private async Task TappedComponent(Component selectedComponent)
        {
            SelectedComponents.Add(selectedComponent);
            _allComponents.Remove(selectedComponent);
            Components.Remove(selectedComponent);
        }

        private async Task RemoveComponent(Component selectedComponent)
        {
            Components.Add(selectedComponent);
            var temprecords=Components.OrderBy(o => o.Name);
            Components = new ObservableCollection<Component>(temprecords);
            _allComponents =  new ObservableCollection<Component>(temprecords);
            SelectedComponents.Remove(selectedComponent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}