using Microsoft.AspNetCore.Mvc;
using Stripe;
using test.Models;
using System.Text.Json;

namespace test.Controllers
{

    [Route("api/[controller]/[action]")]
    public class WebhookController : Controller
    {





        private readonly IUpdateService _updateService;


        public WebhookController(IUpdateService updateService)
        {
            _updateService = updateService;
        }










        [HttpPost]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            // Log the entire payload
            Console.WriteLine($"Webhook Payload: {json}");

            Console.WriteLine("In the STRIPEWEBHGOOK");

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded || stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    if (paymentIntent != null)
                    {
                        // Now you can access paymentIntent.Metadata
                        var metadata = paymentIntent.Metadata;
                        // Access the metadata to get the list of products

                        if (metadata.ContainsKey("products"))
                        {
                            var productsJson = metadata["products"];
                            var products = JsonSerializer.Deserialize<List<cartItem>>(productsJson);

                            // Now you have the list of products to update in your database
                            foreach (var product in products)
                            {
                                // Update the product in your database
                                await _updateService.UpdateProduct(new Models.Product(Id: product.id, Name: product.name, Description: product.description, Category: product.category, Price: product.price, Additional_inf: product.additional_inf, Quantity: product.OriginalQuantity - product.CartQuantity));
                            }
                        }

                    }

               



                    Console.WriteLine("Suceees");
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

















        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_498bfacb954dc01ad2c558c5ec7f8e800e0c27aecf1f378a2686cc71184e7386";

        [HttpPost]
        public async Task<IActionResult> StripeWebhookTwo()
        {

            Console.WriteLine("In the Index METHOD");

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Data);

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }











































    }
}
