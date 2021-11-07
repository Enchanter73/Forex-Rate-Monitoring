using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Data {
    public class DBReader {
        public static IEnumerable<ExchangeRateModel> GetFromDB(FER_Context ctx)
        {
            return ctx.ExchangeRates;
        }
    }
}
