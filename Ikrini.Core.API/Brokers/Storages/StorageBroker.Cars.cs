using Ikrini.Core.API.Models.Cars;
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

        public ValueTask<Car> UpdateCarAsync(Car car)
        {
            throw new NotImplementedException();
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
