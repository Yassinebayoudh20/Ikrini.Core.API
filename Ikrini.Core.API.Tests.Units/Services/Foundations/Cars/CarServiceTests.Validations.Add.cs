using FluentAssertions;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Moq;
using System;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCarIsNullAndLogItAsync()
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


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfCarIsInvalidAndLogItAsync(string invalidString)
        {
            //Arrange
            var invalidCar = new Car()
            {
                Id = Guid.Empty,
                Brand = invalidString,
                Model = invalidString,
                PlateNumber = invalidString,
                Color = invalidString,
                Year = 0,
                PricePerDay = 0.0M,
                OwnerId = Guid.Empty
            };

            var invalidCarException =
                new InvalidCarException(
                    message: "Car is invalid, fix the errors and try again.");

            invalidCarException.Data.Add(key: nameof(Car.Id), value: "Id is invalid");
            invalidCarException.Data.Add(key: nameof(Car.OwnerId), value: "Id is invalid");
            invalidCarException.Data.Add(key: nameof(Car.Brand), value: "Text is required");
            invalidCarException.Data.Add(key: nameof(Car.Model), value: "Text is required");
            invalidCarException.Data.Add(key: nameof(Car.PlateNumber), value: "Text is required");
            invalidCarException.Data.Add(key: nameof(Car.Color), value: "Text is required");
            invalidCarException.Data.Add(key: nameof(Car.Year), value: "Number is required");
            invalidCarException.Data.Add(key: nameof(Car.PricePerDay), value: "Number is required");

            var expectedCarValidationException =
                new CarValidationException(
                    message: "Car validation error occurred, fix the errors and try again.",
                    innerException: invalidCarException);

            // Act
            ValueTask<Car> addCarTask =
                this.carService.AddCarAsync(invalidCar);

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
