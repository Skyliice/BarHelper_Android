using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace BarHelper_Android.Models
{
    public class Liq : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "liqID")]
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            } 
        }

        private string _amount;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}