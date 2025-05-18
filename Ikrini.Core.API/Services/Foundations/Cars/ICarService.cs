using Ikrini.Core.API.Models.Foundations.Cars;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    public interface ICarService
    {
        ValueTask<IQueryable<Car>> RetrieveAllCarsAsync();
    }
}
