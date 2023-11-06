

using Stripe;
using Stripe.Checkout;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [Route("VIRK MAND")]
        [ApiController]
        public class CheckoutApiController : Controller
        {
            [HttpPost]
            public ActionResult Create()
            {
                var domain = "http://localhost:4242";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "{{PRICE_ID}}",
                    Quantity = 1,
                  },
                },
                    Mode = "payment",
                    SuccessUrl = domain + "/success.html",
                    CancelUrl = domain + "/cancel.html",
                };
                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
        }
    }
}
