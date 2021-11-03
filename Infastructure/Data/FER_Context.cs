using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infastructure
{
    public class FER_Context: DbContext
    {
        public FER_Context(): base("ForexDB")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<FER_Context, Migrations.Configuration>());
        }

        public DbSet<ExchangeRateModel> CurrentExchangeRates { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (Type type in ...)
            {
                entityMethod.MakeGenericMethod(type)
                    .Invoke(modelBuilder, new object[] { });
            }
            base.OnModelCreating(modelBuilder);    

        }*/
    }

}
