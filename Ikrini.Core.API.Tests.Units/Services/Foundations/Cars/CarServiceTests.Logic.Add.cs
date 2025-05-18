// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Ikrini.Core.API.Models.Foundations.Cars;
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
        public async Task ShouldAddCarAsync()
        {
            //Arrange
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset now = randomDateTimeOffset;
            Car randomCar = CreateRandomCar(dateTimeOffset: randomDateTimeOffset);
            Car inputCar = randomCar;
            Car insertedCar = inputCar.DeepClone();
            Car expectedCar = insertedCar.DeepClone();

            this.storageBrokerMock.Setup(broker => 
            broker.InsertCarAsync(inputCar))
                .ReturnsAsync(insertedCar);

            //Act
            Car actualCar = 
                await this.carService.AddCarAsync(inputCar);

            //Assert
            actualCar.Should().BeEquivalentTo(expectedCar);

            this.storageBrokerMock.Verify(broker => 
                broker.InsertCarAsync(inputCar), 
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.datetimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
