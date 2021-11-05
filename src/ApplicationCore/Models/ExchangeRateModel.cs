using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ExchangeRateModel
    {
        [Key]
        public int ExchangeId { get; set; }
        
        [Column("FromCurrencyCodeId")]
        [JsonProperty("1. From_Currency Code")]
        public string FromCurrencyCode { get; set; }

        [Column("ToCurrencyCodeId")]
        [JsonProperty("3. To_Currency Code")]
        public string ToCurrencyCode { get; set; }

        [Column("ExchangeRate")]
        [JsonProperty("5. Exchange Rate")]
        public string ExchangeRate { get; set; }

        [Column("Date")]
        [JsonProperty("6. Last Refreshed")]
        public DateTime Date { get; set; }
    }
}
