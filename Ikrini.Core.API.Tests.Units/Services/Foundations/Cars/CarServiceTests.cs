using Ikrini.Core.API.Brokers.Datetimes;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDatetimeBroker> datetimeBrokerMock;
        private readonly ICarService carService;

        public CarServiceTests()
        {
            this.storageBrokerMock =
                new Mock<IStorageBroker>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.datetimeBrokerMock =
                new Mock<IDatetimeBroker>();

            this.carService = new CarService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                datetimeBroker : this.datetimeBrokerMock.Object);
        }

        private IQueryable<Car> CreateRandomCars()
        {
            return CreateCarFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                .AsQueryable();
        }

        private static string GetRandomString() { 
            return new MnemonicString().GetValue();
        }

        private static Car CreateRandomCar() =>
            CreateRandomCar(dateTimeOffset: GetRandomDateTimeOffset());


        private static Car CreateRandomCar(DateTimeOffset dateTimeOffset) =>
            CreateCarFiller(dateTimeOffset).Create();

        private SqlException CreateSqlException() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
             actualException => actualException.SameExceptionAs(expectedException);

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Car> CreateCarFiller(DateTimeOffset dateTimeOffset)
        {
            Guid userId = Guid.NewGuid();
            string userName = GetRandomString();
            int minYear = 1900;
            int maxYear = DateTimeOffset.UtcNow.Year;
            var filler = new Filler<Car>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTimeOffset)
                .OnProperty(car => car.CreatedBy).Use(userName)
                .OnProperty(car => car.UpdatedBy).Use(userName)
                .OnProperty(car => car.OwnerId).Use(userId)
                .OnProperty(car => car.Year).Use(new IntRange(min: minYear , max : maxYear).GetValue);

            return filler;
        }

    }
}
