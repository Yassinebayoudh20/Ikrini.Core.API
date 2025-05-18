using Ikrini.Core.API.Models.Foundations.Bookings;
using System;
using System.Collections.Generic;

namespace Ikrini.Core.API.Models.Foundations.Cars
{
    public class Car : IKey, IAudit
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public Guid OwnerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
    }
}