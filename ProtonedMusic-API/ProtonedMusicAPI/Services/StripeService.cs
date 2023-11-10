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

            var service = new SessionService();
            var session = service.Create(options);

            return session.Id;
        }
    }
}
