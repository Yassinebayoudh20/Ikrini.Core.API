// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Ikrini.Core.API.Controllers;
using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Moq;
using RESTFulSense.Clients.Extensions;
using RESTFulSense.Controllers;
using RESTFulSense.Models;
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
        [Theory]
        [MemberData(nameof(ServerException))]
        public async Task ShouldReturnInternalServerErrorOnExceptionAsync(Xeption serverException)
        {
           // Arrange

            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                    InternalServerError(serverException);

            this.carServiceMock.Setup(service =>
                service.RetrieveAllCarsAsync())
                    .ThrowsAsync(serverException);

            // Act

            ActionResult<IQueryable<Car>> actualActionResult =
                await this.carsController.GetAllCarsAsync();

            // Assert

            actualActionResult.Should().BeEquivalentTo(expectedInternalServerErrorObjectResult);

            this.carServiceMock.Verify(service =>
                service.RetrieveAllCarsAsync(),
                    Times.Once());

            this.carServiceMock.VerifyNoOtherCalls();
        }
    }
}
