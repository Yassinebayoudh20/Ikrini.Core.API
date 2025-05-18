using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
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
                (Rule: IsInvalidYear(car.Year), Parameter: nameof(Car.Year)),
                (Rule: IsInvalidPricePerDay(car.PricePerDay), Parameter: nameof(Car.PricePerDay)),
                (Rule: IsInvalid(car.OwnerId), Parameter: nameof(Car.OwnerId)),
                (Rule: IsInvalid(car.CreatedBy), Parameter: nameof(Car.CreatedBy)),
                (Rule: IsInvalid(car.UpdatedBy), Parameter: nameof(Car.UpdatedBy)),
                (Rule: IsInvalid(car.CreatedDate), Parameter: nameof(Car.CreatedDate)),
                (Rule: IsInvalid(car.UpdatedDate), Parameter: nameof(Car.UpdatedDate)),
                (
                    Rule: IsDateNotSame(
                    createdDate: car.CreatedDate,
                    updatedDate :car.UpdatedDate, 
                    createdDateName :nameof(Car.CreatedDate)),
                    Parameter: nameof(Car.UpdatedDate)
                ),
                (
                    Rule: IsValuesNotSame(
                    createdBy :car.CreatedBy,
                    updatedBy: car.UpdatedBy,
                    createdByName: nameof(Car.CreatedBy)),
                    Parameter: nameof(Car.UpdatedBy)
                ));
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

        private static dynamic IsInvalid(DateTimeOffset dateTime) => new
        {
            Condition = dateTime == default,
            Message = "Date is invalid"
        };

        private static dynamic IsDateNotSame(
            DateTimeOffset createdDate,
            DateTimeOffset updatedDate,
            string createdDateName) => new
            {
                Condition = createdDate != updatedDate,
                Message = $"Date is not the same as {createdDateName}"
            };

        private static dynamic IsValuesNotSame(
            string createdBy,
            string updatedBy,
            string createdByName) => new
            {
                Condition = createdBy != updatedBy,
                Message = $"Text is not the same as {createdByName}"
            };

        private static dynamic IsInvalidYear(int year) => new
        {
            Condition = IsValidYear(year) is false,
            Message = "Year is invalid"
        };

        private static dynamic IsInvalidPricePerDay(decimal pricePerDay) => new
        {
            Condition = IsValidPricePerDay(pricePerDay) is false,
            Message = "Price Per Day is invalid"
        };

        private static bool IsValidYear(int year)
        {
            var currentYear = DateTimeOffset.UtcNow.Year;
            return year.CompareTo(default) > 0 && year >= 1900 && year <= currentYear;
        }

        private static bool IsValidPricePerDay(decimal pricePerDay)
        {
            return pricePerDay.CompareTo(default) > 0;
        }

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
