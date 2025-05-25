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
        public async Task ShouldModifyCarAsync()
        {
            //Arrange
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            Car randomCar = CreateRandomCar(dateTimeOffset: randomDateTimeOffset);
            Car inputCar = randomCar.DeepClone();
            Car storageCar = randomCar.DeepClone();
            storageCar.UpdatedDate = storageCar.CreatedDate;
            Car updatedCar = inputCar.DeepClone();
            Car expectedCar = updatedCar.DeepClone();

            this.datetimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectCarByIdAsync(inputCar.Id))
                    .ReturnsAsync(storageCar);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateCarAsync(inputCar))
                    .ReturnsAsync(updatedCar);

            //Act
            Car actualCar = 
                await this.carService.ModifyCarAsync(inputCar);

            //Assert
            actualCar.Should().BeEquivalentTo(expectedCar);

            this.datetimeBrokerMock.Verify(broker => 
                broker.GetCurrentDateTimeOffsetAsync(), 
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCarByIdAsync(inputCar.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker => 
                broker.UpdateCarAsync(inputCar), 
                    Times.Once);

            this.datetimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
