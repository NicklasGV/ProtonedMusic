using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using System.Text;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly string _stripeSecretKey = "sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F";
        const string endpointSecret = "whsec_UvmXZFPvh8CXbjaJuYM3C9Z1EiTiwe0m";
        public required Customer _customer;

        public class ChargeInfo
        {
            public string Id { get; set; }
            public string receiptUrl { get; set; }
            public string Status { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PaymentIntent was successful!");
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("PaymentMethod was attached to a Customer!");
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("charges/{customerEmail}")]
        public IActionResult GetChargesByCustomer(string customerEmail)
        {
            StripeConfiguration.ApiKey = _stripeSecretKey;

            _customer = GetOrCreateCustomer(customerEmail);

            if (_customer == null)
            {
                return NotFound("Customer not found");
            }

            var service = new ChargeService();
            try
            {
                var options = new ChargeListOptions
                {
                    Customer = _customer.Id,
                };
                var charges = service.List(options);

                var receiptUrls = charges.Select(charge => charge.ReceiptUrl).ToList();

                return Ok(receiptUrls);
            }
            catch (StripeException e)
            {
                // Handle Stripe API errors
                return StatusCode(500, e.Message);
            }
        }

        private Customer GetOrCreateCustomer(string email)
        {
            var existingCustomer = FindCustomerByEmail(email);

            if (existingCustomer != null)
            {
                return existingCustomer;
            }

            var customerOptions = new CustomerCreateOptions
            {
                Email = email,
                Description = "Guest customer",

            };

            var customerService = new CustomerService();
            return customerService.Create(customerOptions);
        }

        private Customer FindCustomerByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var customerService = new CustomerService();
            var customer = customerService.List(new CustomerListOptions { Email = email });

            return customer.Last();
        }

    }
}
