using Stripe;
using Stripe.Checkout;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public class CheckoutSessionDto
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
        }

        [HttpPost("createCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutSessionDto model)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F";

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(model.Amount * 100), // Beløb i ører
                            Currency = "dkk",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Produktnavn"
                            }
                        },
                        Quantity = 1
                    }
                },
                    Mode = "payment",
                    SuccessUrl = "https://dinwebsite.com/success",
                    CancelUrl = "https://dinwebsite.com/cancel",
                };

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
