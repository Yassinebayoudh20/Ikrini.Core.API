// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Foundations.Cars;
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
        public async Task ShouldRetirveCarByIdAsync()
        {
            // Arrange (preparation of data)
            Car randomCar = CreateRandomCar();

            Car storageCar = randomCar; // Cars that are supposed to be in DB

            Car expectedCar = storageCar.DeepClone(); // Cars that are expected to be returned

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCarByIdAsync(randomCar.Id))
                    .ReturnsAsync(storageCar);

            // Act (execution of the service method)
            Car actualCar =
                await this.carService.RetriveCarByIdAsync(randomCar.Id);

            // Assert (verification of the result)
            actualCar.Should().BeEquivalentTo(expectedCar);

            //Verifiy that the method was called once
            this.storageBrokerMock.Verify(broker =>
                broker.SelectCarByIdAsync(randomCar.Id),
                    Times.Once);

            // Verify that no other methods were called
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.datetimeBrokerMock.VerifyNoOtherCalls();
        }

    }
}
