using FluentAssertions;
using Force.DeepCloner;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfDependencyFailureOccuredAndLogItAsync()
        {
           //Arrange

            SqlException sqlException = CreateSqlException();

            FailedCarStorageException expectedCarStorageException =
                new FailedCarStorageException(
                    message: "Failed Car storage occurred , contact support.",
                    innerException :  sqlException);
            
            CarDependencyException expectedcarDependencyException = 
                new CarDependencyException(
                    message: "Car dependency error occurred, contact support.",
                    innerException: expectedCarStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCarsAsync())
                    .ThrowsAsync(sqlException);

            //Act

            ValueTask<IQueryable<Car>> retrieveAllCarsTask =
                this.carService.RetrieveAllCarsAsync();

            CarDependencyException actualCarDependencyException =
                await Assert.ThrowsAsync<CarDependencyException>(retrieveAllCarsTask.AsTask);

            //Assert

            actualCarDependencyException.Should().BeEquivalentTo(
                expectedcarDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCarsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs( //check if the exception is same as expected)
                    expectedcarDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }

    }
}
