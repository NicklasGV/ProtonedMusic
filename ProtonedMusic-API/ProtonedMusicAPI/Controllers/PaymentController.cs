using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private string stripeSecretKey = "sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F";

        [HttpPost("CreateCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] List<CheckoutItemDto> items)
        {
            try
            {
                StripeConfiguration.ApiKey = stripeSecretKey;

                var lineItems = items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "dkk",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Images = new List<string> { item.Product }
                        },
                        UnitAmount = (long)(item.Price * 100) // Amount in the smallest currency unit (e.g., øre for DKK)
                    },
                    Quantity = item.Quantity
                }).ToList();

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://yourwebsite.com/success.html", // Replace with your frontend success URL
                    CancelUrl = "https://yourwebsite.com/cancel.html", // Replace with your frontend cancel URL
                };

                var service = new SessionService();
                Session session = service.Create(options);

                return Ok(session);
            }
            catch (StripeException stripeEx)
            {
                return BadRequest(new { error = stripeEx.StripeError.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        public class CheckoutItemDto
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public string Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
