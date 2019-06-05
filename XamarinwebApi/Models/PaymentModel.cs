using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XamarinwebApi.Models
{
    public class PaymentModel
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
    }
}