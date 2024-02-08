using ProtonedMusicAPI.Database.NonDatabaseEntities;
using Stripe;
using Stripe.Checkout;
using System;

[ApiController]
[Route("api")]
public class CheckoutController : ControllerBase
{
    private readonly StripeService _stripeService;

    public CheckoutController()
    {
        _stripeService = new StripeService("sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F");
    }


    [HttpPost("CreateCheckoutSession")]
    public IActionResult CreateCheckoutSession([FromBody] CheckoutRequest request)
    {
        try
        {
            Console.WriteLine("Received request to create Checkout Session with items:");
            foreach (var item in request.CartItems)
            {
                Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Unit Amount: {item.UnitAmount}");
            }

            var sessionId = _stripeService.CreateCheckoutSession(request.CartItems, request.CustomerEmail);

            // Returner både SessionId og SuccessUrl til frontend
            return Ok(new { SessionId = sessionId, SuccessUrl = "http://localhost:4200/#/order/success" });
        }
        catch (Exception ex)
        {
            // Log fejl og returner BadRequest-status
            Console.WriteLine($"Error creating Checkout Session: {ex.Message}");
            return BadRequest(new { Error = ex.Message });
        }
    }
}