using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models {
    public class History {
        public int HistoryId { get; set; }

        [Column("FromCurrencyCodeId")]
        public Currency FromCurrencyCode { get; set; }

        [Column("ToCurrencyCodeId")]
        public Currency ToCurrencyCode { get; set; }

        [Column("ExchangeRate")]
        public string ExchangeRate { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("ExchangeId")]
        //public int ExchangeID { get; set; }
        public ExchangeRateModel ExchangeRateModel { get; set; }
    }
}
