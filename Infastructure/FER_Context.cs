using Application_Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infastructure
{
    public class FER_Context: DbContext
    {
        public FER_Context(): base()
        {

        }

        public DbSet<ExchangeRateModel> CurrentExchangeRates { get; set; }

    }
}
