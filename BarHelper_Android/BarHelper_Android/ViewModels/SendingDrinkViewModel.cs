using System.Windows.Input;
using BarHelper_Android.Models;
using Xamarin.Forms;

namespace BarHelper_Android.ViewModels
{
    public class SendingDrinkViewModel
    {
        public Drink NewDrink { get; set; }

        public INavigation Navigation;
        public ICommand BackCommand { get; protected set; }
        public ICommand ConfirmCommand { get; protected set; }
        
        private bool isInternetSourceImage;

        public SendingDrinkViewModel(Drink newDrink, bool _isInternetSourceImage)
        {
            NewDrink = newDrink;
            isInternetSourceImage = _isInternetSourceImage;
            BackCommand = new Command(GoBack);
            ConfirmCommand = new Command(Confirm);
        }

        private async void GoBack()
        {
            await Navigation.PopAsync();
        }

        private async void Confirm()
        {
            var response = await App.Current.MainPage.DisplayAlert("Confirmation",
                "Are you sure you want to add this recipe?", "Yes", "No");
            if (response)
            {
                var dsource = DataSource.getInstance();
                var answer = dsource.AddDrink(NewDrink, isInternetSourceImage);
                if (answer)
                {
                    DependencyService.Get<IMessage>().ShortAlert("The drink was successfully added to the collection!");
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something went wrong...");
                }
            }
        }
    }
}