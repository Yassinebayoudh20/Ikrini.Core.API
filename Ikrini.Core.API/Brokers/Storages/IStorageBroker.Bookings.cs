// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------


using Ikrini.Core.API.Models.Foundations.Bookings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IQueryable<Booking>> SelectAllBookingsAsync();
        ValueTask<Booking> InsertBookingAsync(Booking car);
        ValueTask<Booking> UpdateBookingAsync(Booking car);
        ValueTask<Booking> DeleteBookingAsync(Booking car);
        ValueTask<Booking> SelectBookingByIdAsync(Guid carId);
    }
}
