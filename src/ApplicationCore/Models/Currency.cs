using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [Column("CurrencyName")]
        public string CurrencyName { get; set; }
    }
}
