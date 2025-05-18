using Ikrini.Core.API.Models.Foundations.Bookings;
using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ikrini.Core.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        void AddBookingConfigurations(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(booking => booking.Id);

            builder.HasOne(booking => booking.Car)
                   .WithMany(car => car.Bookings)
                   .HasForeignKey(booking => booking.CarId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
