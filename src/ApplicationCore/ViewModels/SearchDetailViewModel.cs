using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class SearchDetailViewModel
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public string ExchangeRate { get; set; }
    }
}
