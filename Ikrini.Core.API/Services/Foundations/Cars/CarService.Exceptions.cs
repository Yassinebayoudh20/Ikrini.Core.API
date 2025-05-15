using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    internal partial class CarService : ICarService
    {
        private delegate ValueTask<IQueryable<Car>> ReturningCarsFunction();

        private async ValueTask<IQueryable<Car>> TryCatch(
            ReturningCarsFunction returningCarsFunction)
        {
            try
            {
                return await returningCarsFunction();
            }
            catch (SqlException sqlException)
            {
                throw;
            }
        }
    }
}
