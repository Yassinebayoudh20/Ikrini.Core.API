// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------


using Ikrini.Core.API.Models.Cars;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<IQueryable<Car>> SelectAllCarsAsync();
        ValueTask<Car> InsertCarAsync(Car car);
        ValueTask<Car> UpdateCarAsync(Car car);
        ValueTask<Car> DeleteCarAsync(Car car);
        ValueTask<Car> SelectCarByIdAsync(Guid carId);
    }
}
