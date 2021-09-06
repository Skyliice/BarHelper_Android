using Android.App;
using Android.Widget;
using BarHelper_Android.Android;

[assembly: Xamarin.Forms.Dependency(typeof(Message))]
namespace BarHelper_Android.Android
{
    public class Message : IMessage
    {
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}