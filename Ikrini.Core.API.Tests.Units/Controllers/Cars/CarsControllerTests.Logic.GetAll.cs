// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Force.DeepCloner;
using Ikrini.Core.API.Controllers;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Moq;
using RESTFulSense.Clients.Extensions;
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
        [Fact]
        public async Task ShouldReturnOkWithAllCarsAsync()
        {
            // Arrange
            IQueryable<Car> randomCars = 
                    CreateRandomCars();

            IQueryable<Car> storageCars = 
                    randomCars;

            IQueryable<Car> expectedCars = 
                    storageCars.DeepClone();

            OkObjectResult expectedOkObjectResult = 
                new OkObjectResult(expectedCars);

            this.carServiceMock.Setup(service => 
                service.RetrieveAllCarsAsync())
                    .ReturnsAsync(storageCars);

            //Act

            ActionResult<IQueryable<Car>> actualActionResult =
                await this.carsController.GetAllCarsAsync();

           // Assert
           actualActionResult.ShouldBeEquivalentTo(expectedOkObjectResult);

              this.carServiceMock.Verify(service => 
                 service.RetrieveAllCarsAsync(),
                      Times.Once());

            this.carServiceMock.VerifyNoOtherCalls();
        }
    }
}
