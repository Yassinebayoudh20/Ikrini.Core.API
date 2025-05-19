// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ikrini.Core.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        void AddCarConfigurations(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(car => car.Id);
        }
    }
}
