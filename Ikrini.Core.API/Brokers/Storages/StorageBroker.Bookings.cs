using Ikrini.Core.API.Models.Foundations.Bookings;
using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Storages
{
    internal partial class StorageBroker : IStorageBroker
    {
        public DbSet<Booking> Bookings { get; set; }


        public ValueTask<IQueryable<Booking>> SelectAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Booking> InsertBookingAsync(Booking car)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Booking> UpdateBookingAsync(Booking car)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Booking> DeleteBookingAsync(Booking car)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Booking> SelectBookingByIdAsync(Guid carId)
        {
            throw new NotImplementedException();
        }
    }
}
