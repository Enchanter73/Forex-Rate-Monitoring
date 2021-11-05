using ApplicationCore.Models;
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
            modelBuilder.Entity<Currency>().ToTable("Currency");
            modelBuilder.Entity<ExchangeRateModel>().ToTable("ExchangeRates");     
        }
    }

}
