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

        public string CreateDeliveryAddressSession()
        {
            var options = new SessionCreateOptions
            {
                Locale = "auto",  // Set language to Danish
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "DK" },
                },
                ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 0,
                                Currency = "dkk",  // Set currency to DKK (Danish Krone)
                            },
                            DisplayName = "Gratis fragt",
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 5,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 7,
                                },
                            },
                        },
                    },
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 1500,
                                Currency = "dkk",  // Set currency to DKK
                            },
                            DisplayName = "Næste dags levering",
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                            },
                        },
                    },
                },
                //HALLO DU!
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "dkk",  // Set currency to DKK
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "T-shirt",
                            },
                            UnitAmount = 1000,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            return service.Create(options).Id;
        }

        //Ur OldSchool FoOl
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
    }
}
