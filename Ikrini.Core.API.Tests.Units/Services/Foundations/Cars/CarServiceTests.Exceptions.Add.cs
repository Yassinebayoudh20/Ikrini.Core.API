// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using FluentAssertions;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccurredAndLogItAsync()
        {
            //Arrange
            Car randomCar = CreateRandomCar();
            SqlException sqlException = CreateSqlException();

            var failedCarStorageException =
               new FailedCarStorageException(
                   message: "Failed Car storage occurred , contact support.",
                   innerException: sqlException);

            var expectedCarDependencyException =
                new CarDependencyException(
                    message: "Car dependency error occurred, contact support.",
                    innerException: failedCarStorageException);

            this.datetimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .ThrowsAsync(sqlException);
            //Act

            ValueTask<Car> addCarTask =
                this.carService.AddCarAsync(randomCar);

            CarDependencyException actualCarDependencyException = 
                await Assert.ThrowsAsync<CarDependencyException>(testCode : addCarTask.AsTask);

            //Assert

            actualCarDependencyException.Should().BeEquivalentTo(expectedCarDependencyException);

            this.datetimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedCarDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCarAsync(randomCar),
                    Times.Never);

            this.datetimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccurredAndLogItAsync()
        {
            //Arrange
            Car randomCar = CreateRandomCar();
            Exception serviceException = new Exception();

            var failedCarServiceException =
                new FailedCarServiceException(
                    message: "Failed Car service occurred, contact support.",
                    innerException: serviceException);

            var expectedCarServiceException =
                new CarServiceException(
                    message: "Car service error occurred, contact support.",
                    innerException: failedCarServiceException);

            this.datetimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .ThrowsAsync(serviceException);
            //Act

            ValueTask<Car> addCarTask =
                this.carService.AddCarAsync(randomCar);

            CarServiceException actualCarServiceException =
                await Assert.ThrowsAsync<CarServiceException>(testCode: addCarTask.AsTask);

            //Assert

            actualCarServiceException.Should().BeEquivalentTo(expectedCarServiceException);

            this.datetimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedCarServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCarAsync(randomCar),
                    Times.Never);

            this.datetimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

    }
}
