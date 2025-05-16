using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using System;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    public partial class CarService : ICarService
    {
        private void ValidateCarOnAdd(Car car)
        {
            ValidateCarIsNotNull(car);

            Validate(
                (Rule: IsInvalid(car.Id), Parameter: nameof(Car.Id)),
                (Rule: IsInvalid(car.Brand), Parameter: nameof(Car.Brand)),
                (Rule: IsInvalid(car.Model), Parameter: nameof(Car.Model)),
                (Rule: IsInvalid(car.PlateNumber), Parameter: nameof(Car.PlateNumber)),
                (Rule: IsInvalid(car.Color), Parameter: nameof(Car.Color)),
                (Rule: IsInvalid(car.Year), Parameter: nameof(Car.Year)),
                (Rule: IsInvalid(car.PricePerDay), Parameter: nameof(Car.PricePerDay)),
                (Rule: IsInvalid(car.OwnerId), Parameter: nameof(Car.OwnerId)));
        }


        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is invalid"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number.CompareTo(default) <= 0,
            Message = "Number is required"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number.CompareTo(default) <= 0,
            Message = "Number is required"
        };

        private void ValidateCarIsNotNull(Car car)
        {
            if (car is null)
            {
                throw new NullCarException(
                    message: "Car is Null");
            }
        }

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCarException =
                    new InvalidCarException(
                        message: "Car is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCarException.UpsertDataList(
                        key: parameter,
                        value: rule.Message); ;
                }
            }
            invalidCarException.ThrowIfContainsErrors();
        }
    }
}
