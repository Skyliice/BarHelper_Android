using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace BarHelper_Android.Models
{
    public class Drink : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "id")]
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            } }
        [JsonProperty(PropertyName = "image")]
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            } }
        [JsonProperty(PropertyName = "recipe")]
        public List<Liq> Recipe { get; set; }
        
        private int _id;
        private string _name;
        private string _description;
        private string _image;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}