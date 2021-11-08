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
        public int ExchangeId { get; set; }
        
        public Currency FromCurrency { get; set; }

        public Currency ToCurrency { get; set; }

        [Column("ExchangeRate")]
        public string ExchangeRate { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        public List<History> Histories { get; set; }
    }
}
