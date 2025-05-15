using Ikrini.Core.API.Models.Cars;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    internal interface ICarService
    {
        ValueTask<IQueryable<Car>> RetrieveAllCarsAsync();
    }
}
