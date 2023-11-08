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

        public string CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "dkk", // Erstat med din ønskede valuta
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Dit produkt",
                        },
                        UnitAmount = 1000, // Erstat med det beløb, der skal opkræves
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "https://dit-websted.com/success",
                CancelUrl = "https://dit-websted.com/cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return session.Id;
        }
    }
}
