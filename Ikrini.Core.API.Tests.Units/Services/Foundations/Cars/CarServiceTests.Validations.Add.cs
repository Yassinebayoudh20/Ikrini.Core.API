using FluentAssertions;
using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
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
            this.datetimeBrokerMock.VerifyNoOtherCalls();
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
                OwnerId = Guid.Empty,
                CreatedBy = invalidString,
                CreatedDate = default,
                UpdatedBy = invalidString,
                UpdatedDate = default
            };

            var invalidCarException =
                new InvalidCarException(
                    message: "Car is invalid, fix the errors and try again.");

            invalidCarException.AddData(key: nameof(Car.Id), values: "Id is invalid");
            invalidCarException.AddData(key: nameof(Car.OwnerId), values: "Id is invalid");
            invalidCarException.AddData(key: nameof(Car.Brand), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.Model), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.PlateNumber), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.Color), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.Year), values: "Year is invalid");
            invalidCarException.AddData(key: nameof(Car.PricePerDay), values: "Price Per Day is invalid");
            invalidCarException.AddData(key: nameof(Car.CreatedBy), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.CreatedDate), values: "Date is invalid");
            invalidCarException.AddData(key: nameof(Car.UpdatedBy), values: "Text is required");
            invalidCarException.AddData(key: nameof(Car.UpdatedDate), values: "Date is invalid");

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
            this.datetimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1800)]
        [InlineData(3000)]
        public async Task ShouldThrowValidationExceptionOnAddIfYearIsInvalidAndLogItAsync(int invalidYear)
        {
            // Arrange
            Car randomSource = CreateRandomCar();
            Car invalidCar = randomSource;

            invalidCar.Year = invalidYear;

            var invalidCarException =
                new InvalidCarException(
                    message: "Car is invalid, fix the errors and try again.");

            invalidCarException.AddData(key: nameof(Car.Year), values: "Year is invalid");

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
            this.datetimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public async Task ShouldThrowValidationExceptionOnAddIfPricePerDayIsInvalidAndLogItAsync(decimal invalidPricePerDay)
        {
            // Arrange

            Car randomCar = CreateRandomCar();
            Car invalidCar = randomCar;

            invalidCar.PricePerDay = invalidPricePerDay;

            var invalidCarException =
                new InvalidCarException(
                    message: "Car is invalid, fix the errors and try again.");

            invalidCarException.AddData(key: nameof(Car.PricePerDay), values: "Price Per Day is invalid");

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
            this.datetimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
