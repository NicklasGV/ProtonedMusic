using System;
using System.Collections.Generic;
using ProtonedMusicAPI.Database.NonDatabaseEntities;
using Stripe;
using Stripe.Checkout;

namespace ProtonedMusicAPI.Services
{
    public class StripeService
    {
        private readonly string _stripeSecretKey;

        public StripeService(string stripeSecretKey)
        {
            _stripeSecretKey = stripeSecretKey;
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        public string CreateCombinedSession(CustomerInfoData customerInfo, string previousSessionId, List<CartItemData> cartItems)
        {
            var accountInfoSessionId = CreateAccountInfoSession(customerInfo.Email, customerInfo.Name, customerInfo.Address, customerInfo.Phone);
            var deliveryAddressSessionId = CreateDeliveryAddressSession(previousSessionId);
            var checkoutSessionId = CreateCheckoutSession(cartItems);

            // Returner session-IDs til klienten
            return $"{accountInfoSessionId},{deliveryAddressSessionId},{checkoutSessionId}";
        }

        public string CreateAccountInfoSession(string customerEmail, string customerName, string customerAddress, string customerPhone)
        {
            var customerOptions = new CustomerCreateOptions
            {
                Email = customerEmail,
                Name = customerName,
                Address = new AddressOptions
                {
                    Line1 = customerAddress,
                },
                Phone = customerPhone,
            };

            var customerService = new CustomerService();
            var customer = customerService.Create(customerOptions);

            var options = new SessionCreateOptions
            {
                Customer = customer.Id,
                BillingAddressCollection = "required",
                Mode = "setup",
                Currency = "dkk",
                SuccessUrl = "http://localhost:4200/#/",
                CancelUrl = "https://your-website.com/cancel",
            };

            return CreateSession(options);
        }

        public string CreateDeliveryAddressSession(string previousSessionId)
        {
            var options = new SessionCreateOptions
            {
                BillingAddressCollection = "required",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "DK" },
                },
                Mode = "setup",
                Currency = "dkk",
                SuccessUrl = "http://localhost:4200/#/"
            };

            return CreateSession(options, previousSessionId);
        }

        public string CreateCheckoutSession(List<CartItemData> cartItems)
        {
            var lineItems = cartItems.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "dkk",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name,
                    },
                    UnitAmount = item.UnitAmount * 100,
                },
                Quantity = item.Quantity,
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/#/",
                CancelUrl = "https://your-website.com/cancel",
            };

            return CreateSession(options);
        }

        public string CreateSession(SessionCreateOptions options)
        {
            var service = new SessionService();
            var session = service.Create(options);

            return session.Id;
        }

        public string CreateSession(SessionCreateOptions options, string previousSessionId)
        {
            // Log ud af previousSessionId for fejlfinding
            Console.WriteLine($"Previous Session ID: {previousSessionId}");

            // Hvis der er en tidligere session-ID, kan du inkludere det som en reference
            options.SetupIntentData = previousSessionId != null
                ? new SessionSetupIntentDataOptions { Metadata = new Dictionary<string, string> { { "previous_session_id", previousSessionId } } }
                : null;

            var service = new SessionService();
            var session = service.Create(options);

            return session.Id;
        }
    }
}
