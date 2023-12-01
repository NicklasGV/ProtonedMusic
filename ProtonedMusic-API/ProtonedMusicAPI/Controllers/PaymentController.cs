using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProtonedMusicAPI.Services;
using ProtonedMusicAPI.Database.NonDatabaseEntities;

[ApiController]
[Route("api")]
public class CheckoutController : ControllerBase
{
    private readonly StripeService _stripeService;

    public CheckoutController()
    {
        _stripeService = new StripeService("sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F");
    }

    [HttpPost("CreateCombinedSession")]
    public IActionResult CreateCombinedSession([FromBody] CombinedSessionData combinedData)
    {
        try
        {
            var accountInfoSessionId = _stripeService.CreateAccountInfoSession(
                combinedData.CustomerInfo.Email,
                combinedData.CustomerInfo.Name,
                combinedData.CustomerInfo.Address,
                combinedData.CustomerInfo.Phone
            );

            var deliveryAddressSessionId = _stripeService.CreateDeliveryAddressSession(combinedData.PreviousSessionId);
            var checkoutSessionId = _stripeService.CreateCheckoutSession(combinedData.CartItems);

            return Ok(new
            {
                AccountInfoSessionId = accountInfoSessionId,
                DeliveryAddressSessionId = deliveryAddressSessionId,
                CheckoutSessionId = checkoutSessionId
            });
        }
        catch (Exception ex)
        {
            // Log fejl og returner BadRequest-status
            Console.WriteLine($"Error creating combined sessions: {ex.Message}");
            return BadRequest(new { Error = "An error occurred while creating combined sessions. Please try again or contact support." });
        }
    }

    [HttpPost("CreateAccountInfoSession")]
    public IActionResult CreateAccountInfoSession([FromBody] CustomerInfoData customerInfo)
    {
        try
        {
            var sessionId = _stripeService.CreateAccountInfoSession(customerInfo.Email, customerInfo.Name, customerInfo.Address, customerInfo.Phone);
            return Ok(new { SessionId = sessionId });
        }
        catch (Exception ex)
        {
            // Log fejl og returner BadRequest-status
            Console.WriteLine($"Error creating Account Info Session: {ex.Message}");
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("CreateDeliveryAddressSession")]
    public IActionResult CreateDeliveryAddressSession([FromBody] string previousSessionId)
    {
        try
        {
            var sessionId = _stripeService.CreateDeliveryAddressSession(previousSessionId);
            return Ok(new { SessionId = sessionId });
        }
        catch (Exception ex)
        {
            // Log fejl og returner BadRequest-status
            Console.WriteLine($"Error creating Delivery Address Session: {ex.Message}");
            return BadRequest(new { Error = ex.Message });
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

    public class CombinedSessionData
    {
        public CustomerInfoData CustomerInfo { get; set; }
        public string PreviousSessionId { get; set; }
        public List<CartItemData> CartItems { get; set; }
    }

}
