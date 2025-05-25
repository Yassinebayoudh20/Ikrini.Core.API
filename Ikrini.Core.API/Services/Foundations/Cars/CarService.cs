// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Brokers.Datetimes;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Foundations.Cars;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    public partial class CarService : ICarService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDatetimeBroker datetimeBroker;

        public CarService(IStorageBroker storageBroker, ILoggingBroker loggingBroker, IDatetimeBroker datetimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.datetimeBroker = datetimeBroker;
        }

        public ValueTask<IQueryable<Car>> RetrieveAllCarsAsync() =>
            TryCatch(async () =>
            {
                return await this.storageBroker.SelectAllCarsAsync();
            });

        public ValueTask<Car> AddCarAsync(Car car) =>
            TryCatch(async () =>
            {
                await ValidateCarOnAddAsync(car);

                return await this.storageBroker.InsertCarAsync(car);
            });

        public ValueTask<Car> RetriveCarByIdAsync(Guid carId)
        {
            throw new NotImplementedException();
        }
    }
}
