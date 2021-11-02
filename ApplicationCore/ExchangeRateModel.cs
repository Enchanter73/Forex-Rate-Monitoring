using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public class ExchangeRateModel
    {
        public int ExchangeRateModelId { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public string ExchangeRate { get; set; }
        public DateTime Date { get; set; }
    }
}
