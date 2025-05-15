using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Ikrini.Core.API.Tests.Units.Services.Foundations.Cars
{
    public partial class CarServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICarService carService;

        public CarServiceTests()
        {
            this.storageBrokerMock =
                new Mock<IStorageBroker>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.carService = new CarService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private  IQueryable<Car> CreateRandomCars()
        {
            return CreateCarFiller().Create(count: GetRandomNumber()).AsQueryable();
        }

        private SqlException CreateSqlException() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private static Expression<Func<Xeption,bool>> SameExceptionAs(Xeption expectedException) =>
             actualException => actualException.SameExceptionAs(expectedException);

        private int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Filler<Car> CreateCarFiller()
        {
            var filler = new Filler<Car>();

            filler.Setup()
                .OnType<DateTimeOffset>().IgnoreIt();
   
            return filler;
        }

    }
}
