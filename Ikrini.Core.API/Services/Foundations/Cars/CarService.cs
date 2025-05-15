using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Models.Cars;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Services.Foundations.Cars
{
    internal partial class CarService : ICarService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CarService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IQueryable<Car>> RetrieveAllCarsAsync() =>
             await this.storageBroker.SelectAllCarsAsync();
    }
}
