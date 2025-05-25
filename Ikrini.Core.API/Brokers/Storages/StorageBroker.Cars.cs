// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Models.Foundations.Bookings;
using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        public DbSet<Car> Cars { get; set; }

        public async ValueTask<IQueryable<Car>> SelectAllCarsAsync() => 
            await this.SelectAll<Car>();

        public async ValueTask<Car> InsertCarAsync(Car car)
        {
            return await this.InsertAsync<Car>(car);
        }

        public async ValueTask<Car> UpdateCarAsync(Car car)
        {
            return await this.UpdateAsync(car);
        }

        public ValueTask<Car> DeleteCarAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Car> SelectCarByIdAsync(Guid carId)
        {
            throw new NotImplementedException();
        }
    }
}
