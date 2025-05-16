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
        }

        private void ValidateCarIsNotNull(Car car)
        {
            if(car is null)
            {
                throw new NullCarException(
                    message: "Car is Null");
            }
        }
    }
}
