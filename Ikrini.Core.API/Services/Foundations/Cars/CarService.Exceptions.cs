using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    public partial class CarService : ICarService
    {
        private delegate ValueTask<Car> ReturningCarFunction();

        private delegate ValueTask<IQueryable<Car>> ReturningCarsFunction();

        private async ValueTask<Car> TryCatch(
            ReturningCarFunction returningCarFunction)
        {
            try
            {
                return await returningCarFunction();
            }
            catch (NullCarException nullCarException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCarException);
            }
            catch (InvalidCarException invalidCarException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCarException);
            }
            catch (NotFoundCarException notFoundCarException)
            {
                throw await CreateAndLogValidationExceptionAsync(notFoundCarException);
            }
            catch (SqlException sqlException)
            {
                var failedCarStorageException =
                    new FailedCarStorageException(
                        message: "Failed Car storage occurred , contact support.",
                        innerException: sqlException);

                throw await CreateAndLogDependencyExceptionAsync(failedCarStorageException);
            }
            catch (Exception exception)
            {
                var failedCarServiceException =
                    new FailedCarServiceException(
                        message: "Failed Car service occurred, contact support.",
                        innerException: exception);
                throw await CreateAndLogServiceExceptionAsync(failedCarServiceException);
            }
        }

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
            catch (Exception exception)
            {
                var failedCarServiceException =
                    new FailedCarServiceException(
                        message: "Failed Car service occurred, contact support.",
                        innerException: exception);
                throw await CreateAndLogServiceExceptionAsync(failedCarServiceException);
            }
        }

        private async ValueTask<CarValidationException> CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var carValidationException =
                new CarValidationException(
                    message: "Car validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(carValidationException);

            return carValidationException;
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
