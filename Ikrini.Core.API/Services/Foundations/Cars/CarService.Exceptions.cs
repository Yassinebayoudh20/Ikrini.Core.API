// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using EFxceptions.Models.Exceptions;
using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
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

        private async ValueTask<Car> TryCatch(ReturningCarFunction returningCarFunction)
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
            catch (SqlException sqlException)
            {
                var failedCarStorageException =
                    new FailedCarStorageException(
                        message: "Failed Car storage occurred , contact support.",
                        innerException: sqlException);

                throw await CreateAndLogDependencyExceptionAsync(failedCarStorageException);
            }
            catch (DuplicateKeyException duplicatedKeyException)
            {
                var alreadyExistsCarException = new AlreadyExistsCarException(
                    message: "Car already exist error occurred, contact support.",
                    innerException: duplicatedKeyException,
                    data: duplicatedKeyException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsCarException);
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

        private async ValueTask<AlreadyExistsCarException> CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var carDependencyValidationException =
                new CarDependencyValidationException(
                    message: "Car dependency validation error occurred, contact support.",
                    innerException: exception);
            await this.loggingBroker.LogErrorAsync(carDependencyValidationException);
            throw carDependencyValidationException;
        }
    }
}
