using Ikrini.Core.API.Models.Foundations.Cars;
using Ikrini.Core.API.Models.Foundations.Cars.Exceptions;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : RESTFulController
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService) =>
            this.carService = carService;


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

        [HttpPost]
        public async ValueTask<ActionResult<Car>> PostCarAsync(Car car)
        {
            try
            {
                Car addedCar = await this.carService.AddCarAsync(car);
                return Created(addedCar);
            }
            catch (CarValidationException carValidationException)
            {
                return BadRequest(carValidationException.InnerException);
            }
            catch (CarDependencyValidationException carDependencyValidationException)
                when (carDependencyValidationException.InnerException is AlreadyExistsCarException)
            {
                return Conflict(carDependencyValidationException.InnerException);
            }
            catch (CarDependencyValidationException carDependencyValidationException)
            {
                return BadRequest(carDependencyValidationException.InnerException);
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
