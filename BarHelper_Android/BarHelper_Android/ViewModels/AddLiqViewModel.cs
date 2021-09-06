using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Component = BarHelper_Android.Models.Component;

namespace BarHelper_Android.ViewModels
{
    public class AddLiqViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Component> _components;
        public ObservableCollection<Component> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged("Components");
            }
        }

        private string _componentName;
        public string ComponentName
        {
            get => _componentName;
            set
            {
                _componentName = value;
                OnPropertyChanged("ComponentName");
            }
        }

        public ICommand ComponentNameChanged { get; protected set; }
        public ICommand ResetButtonCommand { get; protected set; }
        public ICommand CreateButtonCommand { get; protected set; }
        private ObservableCollection<Component> _allComponents { get; set; }

        public AddLiqViewModel()
        {
            ComponentNameChanged = new Command(ChangeComponentName);
            CreateButtonCommand = new Command(CreateLiq);
            ResetButtonCommand = new Command(ResetView);
            var dsource = DataSource.getInstance();
            _allComponents = new ObservableCollection<Component>();
            var templst = dsource.GetComponents();
            Components = new ObservableCollection<Component>();
            foreach (var component in templst)
            {
                _allComponents.Add(component);
            }   
        }

        private void CreateLiq()
        {
            if (ComponentName == String.Empty)
            {
                DependencyService.Get<IMessage>().ShortAlert("Enter the name");
                return;
            }
            if (_allComponents.Any(o => o.Name == ComponentName))
            {
                DependencyService.Get<IMessage>().ShortAlert("The component is already existing!");
                return;
            }
            var dsource = DataSource.getInstance();
            if (dsource.AddLiq(ComponentName))
            {
                dsource.SetValues();
                DependencyService.Get<IMessage>().ShortAlert("A new component has been added successfully!");
            }
            else
                DependencyService.Get<IMessage>().ShortAlert("Error");
        }

        private void ResetView()
        {
            ComponentName=String.Empty;
            Components.Clear();
        }
        private void ChangeComponentName()
        {
            if (ComponentName == String.Empty)
            {
                Components.Clear();
                return;
            }
            var tempcollection = _allComponents.Where(o => o.Name.ToLower().Contains(ComponentName.ToLower()));
            Components = new ObservableCollection<Component>(tempcollection);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}