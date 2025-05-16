using FluentAssertions;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Moq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfCarIsNullAndLogItAsync()
        {
            // Arrange
            Car nullCar = null;

            var nullCarException =
                new NullCarException(
                    message: "Car is Null");

            var expectedCarValidationException =
                new CarValidationException(
                    message: "Car validation error occurred, fix the errors and try again.",
                    innerException: nullCarException);

            // Act
            ValueTask<Car> addCarTask =
                this.carService.AddCarAsync(nullCar);

            CarValidationException actualCarValidationException =
                await Assert.ThrowsAsync<CarValidationException>(addCarTask.AsTask);

            // Assert
            actualCarValidationException.Should().BeEquivalentTo(expectedCarValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedCarValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCarAsync(It.IsAny<Car>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
