using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infastructure
{
    public class FER_Context: DbContext
    {
        public FER_Context(DbContextOptions<FER_Context> options) : base(options)
        {          
        }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<ExchangeRateModel> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().ToTable("Currency").HasKey(c => new { c.CurrencyId });
            modelBuilder.Entity<Currency>().HasMany(a => a.FromExchangeRateModels).WithOne(b => b.FromCurrency);
            modelBuilder.Entity<Currency>().HasMany(a => a.ToExchangeRateModels).WithOne(b => b.ToCurrency);

            modelBuilder.Entity<ExchangeRateModel>().ToTable("ExchangeRates").HasKey(c => new { c.ExchangeId });
        }
    }

}
