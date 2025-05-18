using Ikrini.Core.API.Controllers;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.Data.SqlClient;
using Moq;
using RESTFulSense.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Ikrini.Core.API.Tests.Units.Controllers.Cars
{
    public partial class CarsControllerTests : RESTFulController
    {
        private readonly Mock<ICarService> carServiceMock;
        private readonly CarsController carsController;

        public CarsControllerTests()
        {
            this.carServiceMock =
                new Mock<ICarService>();

            this.carsController = new CarsController(
                carService: this.carServiceMock.Object);
        }

        private IQueryable<Car> CreateRandomCars()
        {
            return CreateCarFiller().Create(count: GetRandomNumber()).AsQueryable();
        }

        public static TheoryData<Xeption> ServerException()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption(someMessage);
            return new TheoryData<Xeption>
            {
                new CarDependencyException(
                    someMessage,
                    someInnerException),

                new CarServiceException(
                    someMessage,
                    someInnerException),

                //Add More possible Exceptions
            };
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
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
