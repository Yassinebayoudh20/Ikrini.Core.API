// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using EFxceptions;
using Ikrini.Core.API.Models.Foundations.Bookings;
using Ikrini.Core.API.Models.Foundations.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuild)
        {
            string connectionString = this.configuration.GetConnectionString(name : "DefaultConnection");
            dbContextOptionBuild.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            dbContextOptionBuild.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddCarConfigurations(modelBuilder.Entity<Car>());
            AddBookingConfigurations(modelBuilder.Entity<Booking>());
        }

        private async ValueTask<IQueryable<T>> SelectAll<T>() where T : class => this.Set<T>();

        private async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
         await this.FindAsync<T>(objectIds);

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Added;
            await this.SaveChangesAsync();

            return @object;
        }

        private async ValueTask<T> UpdateAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return @object;
        }

        private async ValueTask<T> DeleteAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return @object;
        }
    }
}