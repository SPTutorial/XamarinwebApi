using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinwebApi.Models;
using Stripe;
using System.Web.Mvc;

namespace XamarinwebApi.Controllers
{
    public class PaymentController : ApiController
    {
        [System.Web.Http.HttpPost]
        public Response Post(PaymentModel payment)
        {
            Response res = new Response();
            // You can optionally create a customer first, and attached this to the CustomerId
            var charge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(payment.Amount * 100), // In cents, not dollars, times by 100 to convert
                Currency = "usd", // or the currency you are dealing with
                Description = "something awesome",
                SourceId = payment.Token
            };

            var service = new ChargeService("sk_test_LJ2dJvulsi1f4rsGrqLHCKGC00gP6buBnU");

            try
            {
                var response = service.Create(charge);
                res.Status = 1;
                res.Message = response.Status;

                // Record or do something with the charge information
            }
            catch (StripeException ex)
            {
                StripeError stripeError = ex.StripeError;
                res.Status = 0;
                res.Message = ex.Message;
                // Handle error
            }

            // Ideally you would put in additional information, but you can just return true or false for the moment.
            return res;
        }
    }
}
