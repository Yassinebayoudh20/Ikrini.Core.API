// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    public partial class CarService : ICarService
    {
        private async ValueTask ValidateCarOnAddAsync(Car car)
        {
            ValidateCarIsNotNull(car);

            Validate(
                (Rule: await IsInvalid(car.Id), Parameter: nameof(Car.Id)),
                (Rule: await IsInvalid(car.Brand), Parameter: nameof(Car.Brand)),
                (Rule: await IsInvalid(car.Model), Parameter: nameof(Car.Model)),
                (Rule: await IsInvalid(car.PlateNumber), Parameter: nameof(Car.PlateNumber)),
                (Rule: await IsInvalid(car.Color), Parameter: nameof(Car.Color)),
                (Rule: await IsInvalidYear(car.Year), Parameter: nameof(Car.Year)),
                (Rule: await IsInvalidPricePerDay(car.PricePerDay), Parameter: nameof(Car.PricePerDay)),
                (Rule: await IsInvalid(car.OwnerId), Parameter: nameof(Car.OwnerId)),
                (Rule: await IsInvalid(car.CreatedBy), Parameter: nameof(Car.CreatedBy)),
                (Rule: await IsInvalid(car.UpdatedBy), Parameter: nameof(Car.UpdatedBy)),
                (Rule: await IsInvalid(car.CreatedDate), Parameter: nameof(Car.CreatedDate)),
                (Rule: await IsInvalid(car.UpdatedDate), Parameter: nameof(Car.UpdatedDate)),
                (Rule: await IsNotRecent(car.CreatedDate), Parameter: nameof(Car.CreatedDate)),
                (
                    Rule: await IsDateNotSame(
                    createdDate: car.CreatedDate,
                    updatedDate: car.UpdatedDate,
                    createdDateName: nameof(Car.CreatedDate)),
                    Parameter: nameof(Car.UpdatedDate)
                ),
                (
                    Rule: await IsValuesNotSame(
                    createdBy: car.CreatedBy,
                    updatedBy: car.UpdatedBy,
                    createdByName: nameof(Car.CreatedBy)),
                    Parameter: nameof(Car.UpdatedBy)
                ));
        }


        private async ValueTask<dynamic> IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is invalid"
        };

        private async ValueTask<dynamic> IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private async ValueTask<dynamic> IsInvalid(DateTimeOffset dateTime) => new
        {
            Condition = dateTime == default,
            Message = "Date is invalid"
        };

        private async ValueTask<dynamic> IsDateNotSame(
            DateTimeOffset createdDate,
            DateTimeOffset updatedDate,
            string createdDateName) => new
            {
                Condition = createdDate != updatedDate,
                Message = $"Date is not the same as {createdDateName}"
            };

        private async ValueTask<dynamic> IsValuesNotSame(
            string createdBy,
            string updatedBy,
            string createdByName) => new
            {
                Condition = createdBy != updatedBy,
                Message = $"Text is not the same as {createdByName}"
            };

        private async ValueTask<dynamic> IsNotRecent(DateTimeOffset date) => new
            {
                Condition = await IsDateNotRecent(date),
                Message = "Date is not recent"
            };

        private async ValueTask<dynamic> IsInvalidYear(int year) => new
        {
            Condition = IsValidYear(year) is false,
            Message = "Year is invalid"
        };

        private async ValueTask<dynamic> IsInvalidPricePerDay(decimal pricePerDay) => new
        {
            Condition = IsValidPricePerDay(pricePerDay) is false,
            Message = "Price Per Day is invalid"
        };

        private async ValueTask<bool> IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = await this.datetimeBroker.GetCurrentDateTimeOffsetAsync();
            TimeSpan timeSpanDifference = currentDateTime.Subtract(date);

            return timeSpanDifference.TotalSeconds is > 60 or < 0;
        }

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
