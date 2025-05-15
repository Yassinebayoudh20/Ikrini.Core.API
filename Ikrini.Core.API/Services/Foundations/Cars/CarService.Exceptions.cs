using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

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
                var failedCarStorageException =
                    new FailedCarStorageException(
                        message: "Failed Car storage occurred , contact support.",
                        innerException: sqlException);

                throw await CreateAndLogDependencyException(failedCarStorageException);
            }
        }

        private async ValueTask<CarDependencyException> CreateAndLogDependencyException(Xeption exception)
        {
            var carDependencyException =
                new CarDependencyException(
                    message: "Car dependency error occurred, contact support.",
                    innerException: exception);
            await this.loggingBroker.LogCriticalAsync(carDependencyException);
            throw carDependencyException;
        }
    }
}
