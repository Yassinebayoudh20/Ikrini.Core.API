using Ikrini.Core.API.Models.Foundations.Cars;
using System;

namespace Ikrini.Core.API.Models.Foundations.Bookings
{
    public class Booking : IKey, IAudit
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}