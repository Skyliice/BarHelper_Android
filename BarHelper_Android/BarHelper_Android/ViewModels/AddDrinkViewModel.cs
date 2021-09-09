using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BarHelper_Android.Models;
using BarHelper_Android.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace BarHelper_Android.ViewModels
{
    public class AddDrinkViewModel : INotifyPropertyChanged
    {
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }
        public string DrinkImageLink
        {
            get => _drinkImageLink;
            set
            {
                _drinkImageLink = value;
                OnPropertyChanged("DrinkImageLink");
            }
        }

        public bool IsInternetSource
        {
            get => _isInternetSource;
            set
            {
                _isInternetSource = value;
                OnPropertyChanged("IsInternetSource");
            }
        }

        public INavigation Navigation { get; set; }

        private string _drinkImageLink;
        private bool _isInternetSource;
        
        public ICommand ResetCommand { get; protected set; }
        public ICommand NextCommand { get; protected set; }
        public ICommand UploadCommand { get; protected set; }

        public AddDrinkViewModel()
        {
            ResetCommand = new Command(ResetView);
            NextCommand = new Command(NextView);
            UploadCommand = new Command(UploadImageFromGallery);
            Task.Run(() => CrossMedia.Current.Initialize());
            IsInternetSource = true;
        }

        private void ResetView()
        {
            DrinkName = String.Empty;
            DrinkDescription = String.Empty;
            DrinkImageLink = String.Empty;
            IsInternetSource = true;
        }

        private async void NextView()
        {
            var newDrink = new Drink() {Name = DrinkName, Description = DrinkDescription, Image = DrinkImageLink};
            await Navigation.PushAsync(new AddDrinkRecipeView(newDrink,IsInternetSource));
        }

        private async void UploadImageFromGallery()
        {
            
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DependencyService.Get<IMessage>().ShortAlert("You have to grant permissions to use this feature!");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            try
            {
                var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                DrinkImageLink = selectedImageFile.Path;
                IsInternetSource = false;
            }
            catch (Exception)
            {
                return;
            }
            
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}