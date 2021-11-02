using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore
{
    [Table("ExchangeRates")]
    public class ExchangeRateModel
    {
        [Key]
        public int CurrencyId { get; set; }
        
        [Column("CurrencyCode")]
        [MaxLength(8)]
        public string CurrencyCode { get; set; }

        [Column("ExchangeRate")]
        public string ExchangeRate { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }
    }
}
