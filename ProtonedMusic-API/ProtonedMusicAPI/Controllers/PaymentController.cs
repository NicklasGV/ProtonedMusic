using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProtonedMusicAPI.Services;
using ProtonedMusicAPI.Database.NonDatabaseEntities;
using Stripe.Checkout;
using Stripe;

[ApiController]
[Route("api")]
public class CheckoutController : ControllerBase
{
    private readonly StripeService _stripeService;

    public CheckoutController()
    {
        _stripeService = new StripeService("sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F");
    }

    [HttpPost("CreateDeliveryAddressSession")]
    public IActionResult CreateDeliveryAddressSession()
    {
        try
        {
            // Forsøg at oprette betalingssessionen
            var sessionId = _stripeService.CreateDeliveryAddressSession();

            // Returner session ID til klienten ved succes
            return Ok(new { SessionId = sessionId });
        }
        catch (StripeException e)
        {
            // Håndter fejl fra Stripe API
            return BadRequest(new { ErrorMessage = e.Message });
        }
        catch (Exception ex)
        {
            // Håndter andre uventede fejl
            // Log ex.Message og ex.StackTrace eller håndter på en anden måde
            return StatusCode(500, new { ErrorMessage = "Internal server error" });
        }
    }

    [HttpPost("CreateCheckoutSession")]
    public IActionResult CreateCheckoutSession([FromBody] List<CartItemData> cartItems)
    {
        try
        {
            Console.WriteLine("Received request to create Checkout Session with items:");
            foreach (var item in cartItems)
            {
                Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Unit Amount: {item.UnitAmount}");
            }

            var sessionId = _stripeService.CreateCheckoutSession(cartItems);
            return Ok(new { SessionId = sessionId });
        }
        catch (Exception ex)
        {
            // Log fejl og returner BadRequest-status
            Console.WriteLine($"Error creating Checkout Session: {ex.Message}");
            return BadRequest(new { Error = ex.Message });
        }
    }

}
