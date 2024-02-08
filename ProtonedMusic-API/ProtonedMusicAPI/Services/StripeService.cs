using ProtonedMusicAPI.Database.NonDatabaseEntities;
using Stripe;
using Stripe.Checkout;

namespace ProtonedMusicAPI.Services
{
    public class StripeService
    {
        private readonly string _stripeSecretKey;
        private Customer _customer;

        public StripeService(string stripeSecretKey)
        {
            _stripeSecretKey = stripeSecretKey;
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        public string CreateCheckoutSession(List<CartItemData> cartItems, string customerEmail)
        {
            // Opret kunden (kun hvis den ikke allerede er oprettet)
            if (_customer == null || !string.Equals(_customer.Email, customerEmail, StringComparison.OrdinalIgnoreCase))
            {
                _customer = GetOrCreateCustomer(customerEmail);
            }

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

                SuccessUrl = "http://protonedmusic.com/#/order/success",
                CancelUrl = "http://protonedmusic.com/#/cart",
                Locale = "auto",
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
                                Amount = 5500,
                                Currency = "dkk",
                            },
                            DisplayName = "Forsendelse",
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
                                Amount = 8500,
                                Currency = "dkk",
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
                                }
                            }
                        }
                    }
                },
                CustomerEmail = customerEmail,
            };

            var service = new SessionService();
            var sessionId = service.Create(options).Id;

            // Opret faktura
            var invoiceService = new InvoiceService();

            foreach (var item in cartItems)
            {
                var invoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = _customer.Id,
                    UnitAmountDecimal = (item.Price * 100),
                    Currency = "dkk",
                    Quantity = item.Quantity,
                };

                // Create an invoice item
                new InvoiceItemService().Create(invoiceItemOptions);
            }

            var invoiceOptions = new InvoiceCreateOptions
            {
                Customer = _customer.Id,
                CollectionMethod = "send_invoice",
                DueDate = DateTime.Now,
            };

            var invoice = invoiceService.Create(invoiceOptions);

            var sentInvoice = invoiceService.SendInvoice(invoice.Id);

            return sessionId;
        }

        //Guest Customer
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
                Description = "GU",
                
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
            var customers = customerService.List(new CustomerListOptions { Email = email });

            return customers.FirstOrDefault();
        }
    }
}
