using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.Data.SqlClient;
using System;
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

                throw await CreateAndLogDependencyExceptionAsync(failedCarStorageException);
            }
            catch(Exception exception)
            {
                var failedCarServiceException =
                    new FailedCarServiceException(
                        message: "Failed Car service occurred, contact support.",
                        innerException: exception);
                throw await CreateAndLogServiceExceptionAsync(failedCarServiceException);
            }
        }

        private async ValueTask<CarDependencyException> CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var carDependencyException =
                new CarDependencyException(
                    message: "Car dependency error occurred, contact support.",
                    innerException: exception);
            await this.loggingBroker.LogCriticalAsync(carDependencyException);
            throw carDependencyException;
        }

        private async ValueTask<CarServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var carServiceException =
                new CarServiceException(
                    message: "Car service error occurred, contact support.",
                    innerException: exception);
            await this.loggingBroker.LogErrorAsync(carServiceException);
            throw carServiceException;
        }
    }
}
