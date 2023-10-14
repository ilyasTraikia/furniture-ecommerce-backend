using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace test.Controllers
{


    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {







        [HttpPost]
        public ActionResult Create()
        {
            var domain = "http://localhost:5173";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1KslfNJlodOva1JDr5AlE35L",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
            };

            var service = new SessionService();
            Session session = service.Create(options);

             Response.Headers.Add("Location", session.Url);
             return new StatusCodeResult(303);
           // return Ok(session.Url);
        }






    }





    }
