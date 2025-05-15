using FluentAssertions;
using Force.DeepCloner;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
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
        public async Task ShouldRetirveAllCarsAsync()
        {
            // Arrange (preparation of data)
            IQueryable<Car> randomCars = CreateRandomCars();

            IQueryable<Car> storageCars = randomCars; // Cars that are supposed to be in DB

            IQueryable<Car> expectedCars = storageCars.DeepClone(); // Cars that are expected to be returned

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCarsAsync())
                    .ReturnsAsync(storageCars);

            // Act (execution of the service method)
            IQueryable<Car> actualCars =
                await this.carService.RetrieveAllCarsAsync();

            // Assert (verification of the result)
            actualCars.Should().BeEquivalentTo(expectedCars);

            //Verifiy that the method was called once
            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCarsAsync(),
                    Times.Once);

            // Verify that no other methods were called
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
