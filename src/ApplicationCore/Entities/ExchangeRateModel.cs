using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ExchangeRateModel
    {
        public int ExchangeId { get; set; }
        
        public Currency FromCurrency { get; set; }

        [NotMapped]
        [JsonProperty("base_currency")]
        public string BaseCurreny { get; set; }

        [NotMapped]
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        public Currency ToCurrency { get; set; }

        [JsonProperty("mid")]
        [Column("ExchangeRate")]
        public string ExchangeRate { get; set; }

        [JsonProperty("Date")]
        [Column("Date")]
        public DateTime Date { get; set; }
    }
}
