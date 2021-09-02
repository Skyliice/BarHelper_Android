using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarHelper_Android.Models;
using BarHelper_Android.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarHelper_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkDetailView : ContentPage
    {
        public DrinkDetailView()
        {
            InitializeComponent();
        }
        
        public DrinkDetailView(Drink chosenDrink)
        {
            InitializeComponent();
            BindingContext = new DrinkDetailViewModel(chosenDrink) {Navigation = this.Navigation};
            NavigationPage.SetHasNavigationBar(this,true);
        }
    }
}