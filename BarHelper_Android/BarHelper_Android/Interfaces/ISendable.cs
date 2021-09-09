using System.Threading.Tasks;
using BarHelper_Android.Models;

namespace BarHelper_Android
{
    public interface ISendable
    {
        public Task<bool> AddLiq(string name);
        public Task<bool> AddDrink(Drink newDrink, bool isImageFromInternet);
    }
}