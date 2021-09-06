using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarHelper_Android.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarHelper_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinktionaryView : ContentPage
    {
        public DrinktionaryView()
        {
            InitializeComponent();
            BindingContext = new DrinktionaryViewModel() {Navigation = this.Navigation};
        }
    }
}