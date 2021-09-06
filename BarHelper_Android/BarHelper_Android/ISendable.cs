using System.Threading.Tasks;

namespace BarHelper_Android
{
    public interface ISendable
    {
        public Task<bool> AddLiq(string name);
        public Task AddDrink(string name, string description, string image);
    }
}