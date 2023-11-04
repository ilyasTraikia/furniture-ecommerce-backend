using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.Terminal;
using System.Collections.Generic;
using test.Models;
using System.Text.Json;

namespace test.Controllers
{


    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {


        private readonly IUpdateService _updateService;


        public CheckoutApiController(IUpdateService updateService)
        {
            _updateService = updateService;
        }




        [HttpPost]
        public async Task<ActionResult> Create([FromForm] List<cartItem> cartProducts)
        {
            var domain = "http://localhost:5173";

         

      
     
            // Retrieve the JSON array of objects as a string
            var productsJson = HttpContext.Request.Form["cartProducts"];
            // Deserialize the JSON array into a list of objects
            var products = JsonSerializer.Deserialize<List<cartItem>>(productsJson);

            // Log the received data
            foreach (var product in products)
            {
                Console.WriteLine($"PriceId: {product.price}, Quantity: {product.CartQuantity}");
            }






             var lineItems = products.Select(item => new SessionLineItemOptions
              {
                 PriceData = new SessionLineItemPriceDataOptions
                 {
                     UnitAmountDecimal = item.price * 100, // Assuming item.price is the unit amount in dollars
                     Currency = "usd", // Change this to the appropriate currency code
                     ProductData = new SessionLineItemPriceDataProductDataOptions
                     {
                         Name = item.name, // Product name
                         Description = item.category, // Product description
                                                         // Add more product details as needed
                     },
                 },  // Replace with the appropriate property representing the Price ID
                 Quantity = item.CartQuantity, 
             }).ToList();


            // Log the line items
            foreach (var lineItem in lineItems)
            {
                Console.WriteLine($"line item: {lineItem.PriceData?.UnitAmountDecimal}");
            }













            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
                Metadata = new Dictionary<string, string>
                  {
                    // Serialize the list of products or any other relevant data
                    { "products", JsonSerializer.Serialize(products) }
                  }
            };





            var service = new SessionService();
            Session session = service.Create(options);


   
      

            Response.Headers.Add("Location", session.Url);
             return new StatusCodeResult(303);
           // return Ok(session.Url);
        }


















        // This endpoint is called by Stripe after the payment is successfully processed
        [HttpPost("/checkout/success")]
        public async Task<ActionResult> CheckoutSuccess()
        {
            // Retrieve the JSON array of objects as a string
            var productsJson = HttpContext.Request.Form["cartProducts"];
            // Deserialize the JSON array into a list of objects
            var products = JsonSerializer.Deserialize<List<cartItem>>(productsJson);

            // Update the Products stock in the database
            foreach (var product in products)
            {
                Console.WriteLine("updated");
                await _updateService.UpdateProduct(new Product(Id: product.id, Name: product.name, Description: product.description, Category: product.category, Price: product.price, Additional_inf: product.additional_inf, Quantity: product.OriginalQuantity - product.CartQuantity));
            }

            // Additional logic or redirection after successful payment
            return Ok("Payment successful");
        }































    }





    }
