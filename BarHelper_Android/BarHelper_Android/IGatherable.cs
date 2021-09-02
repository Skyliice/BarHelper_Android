using System.Collections.Generic;
using System.Threading.Tasks;
using BarHelper_Android.Models;

namespace BarHelper_Android
{
    public interface IGatherable
    {
        public Task<List<Drink>> GetAllDrinks();
        public Task<List<Component>> GetAllComponents();
        
    }
}