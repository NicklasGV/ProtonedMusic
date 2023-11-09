using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly StripeService _stripeService;

        public CheckoutController()
        {
            _stripeService = new StripeService("sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F");
        }

        [HttpPost("CreateCheckoutSession")]
        public IActionResult CreateCheckoutSession()
        {
            try
            {
                var sessionId = _stripeService.CreateCheckoutSession();
                return Ok(new { sessionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //HALLO SUT MIG IGOS SUT MIG 
                // saodkfapdsad
            }
        }
    }
}
