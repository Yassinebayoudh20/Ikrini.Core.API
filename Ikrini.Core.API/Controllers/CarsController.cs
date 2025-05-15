using Ikrini.Core.API.Models.Cars;
using Ikrini.Core.API.Models.Cars.Exceptions;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal class CarsController : RESTFulController
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Car>>> GetAllCarsAsync()
        {
            try 
            {
                IQueryable<Car> retievedCars =
                 await this.carService.RetrieveAllCarsAsync();

                return Ok(retievedCars);
            }
            catch (CarDependencyException carDependencyException)
            {
                return InternalServerError(carDependencyException);
            }
            catch (CarServiceException carServiceException)
            {
                return InternalServerError(carServiceException);
            }
        }
    }
}
