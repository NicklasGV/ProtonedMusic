using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_4AFIrs1FokOWreWsnafooZL5gLQqulX5";

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                if (stripeEvent.Type == Events.AccountUpdated)
                {
                }
                else if (stripeEvent.Type == Events.AccountExternalAccountCreated)
                {
                }
                else if (stripeEvent.Type == Events.AccountExternalAccountDeleted)
                {
                }
                else if (stripeEvent.Type == Events.AccountExternalAccountUpdated)
                {
                }
                else if (stripeEvent.Type == Events.BalanceAvailable)
                {
                }
                else if (stripeEvent.Type == Events.BillingPortalConfigurationCreated)
                {
                }
                else if (stripeEvent.Type == Events.BillingPortalConfigurationUpdated)
                {
                }
                else if (stripeEvent.Type == Events.BillingPortalSessionCreated)
                {
                }
                else if (stripeEvent.Type == Events.CapabilityUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CashBalanceFundsAvailable)
                {
                }
                else if (stripeEvent.Type == Events.ChargeCaptured)
                {
                }
                else if (stripeEvent.Type == Events.ChargeExpired)
                {
                }
                else if (stripeEvent.Type == Events.ChargeFailed)
                {
                }
                else if (stripeEvent.Type == Events.ChargePending)
                {
                }
                else if (stripeEvent.Type == Events.ChargeRefunded)
                {
                }
                else if (stripeEvent.Type == Events.ChargeSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.ChargeUpdated)
                {
                }
                else if (stripeEvent.Type == Events.ChargeDisputeClosed)
                {
                }
                else if (stripeEvent.Type == Events.ChargeDisputeCreated)
                {
                }
                else if (stripeEvent.Type == Events.ChargeDisputeFundsReinstated)
                {
                }
                else if (stripeEvent.Type == Events.ChargeDisputeFundsWithdrawn)
                {
                }
                else if (stripeEvent.Type == Events.ChargeDisputeUpdated)
                {
                }
                else if (stripeEvent.Type == Events.ChargeRefundUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CheckoutSessionAsyncPaymentFailed)
                {
                }
                else if (stripeEvent.Type == Events.CheckoutSessionAsyncPaymentSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                }
                else if (stripeEvent.Type == Events.CheckoutSessionExpired)
                {
                }
                else if (stripeEvent.Type == Events.CouponCreated)
                {
                }
                else if (stripeEvent.Type == Events.CouponDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CouponUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CreditNoteCreated)
                {
                }
                else if (stripeEvent.Type == Events.CreditNoteUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CreditNoteVoided)
                {
                }
                else if (stripeEvent.Type == Events.CustomerCreated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CustomerUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerDiscountCreated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerDiscountDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CustomerDiscountUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSourceCreated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSourceDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSourceExpiring)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSourceUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionPaused)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionPendingUpdateApplied)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionPendingUpdateExpired)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionResumed)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerTaxIdCreated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerTaxIdDeleted)
                {
                }
                else if (stripeEvent.Type == Events.CustomerTaxIdUpdated)
                {
                }
                else if (stripeEvent.Type == Events.CustomerCashBalanceTransactionCreated)
                {
                }
                else if (stripeEvent.Type == Events.FileCreated)
                {
                }
                else if (stripeEvent.Type == Events.FinancialConnectionsAccountCreated)
                {
                }
                else if (stripeEvent.Type == Events.FinancialConnectionsAccountDeactivated)
                {
                }
                else if (stripeEvent.Type == Events.FinancialConnectionsAccountDisconnected)
                {
                }
                else if (stripeEvent.Type == Events.FinancialConnectionsAccountReactivated)
                {
                }
                else if (stripeEvent.Type == Events.FinancialConnectionsAccountRefreshedBalance)
                {
                }
                else if (stripeEvent.Type == Events.IdentityVerificationSessionCanceled)
                {
                }
                else if (stripeEvent.Type == Events.IdentityVerificationSessionCreated)
                {
                }
                else if (stripeEvent.Type == Events.IdentityVerificationSessionProcessing)
                {
                }
                else if (stripeEvent.Type == Events.IdentityVerificationSessionRequiresInput)
                {
                }
                else if (stripeEvent.Type == Events.IdentityVerificationSessionVerified)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceCreated)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceDeleted)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceFinalizationFailed)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceFinalized)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceMarkedUncollectible)
                {
                }
                else if (stripeEvent.Type == Events.InvoicePaid)
                {
                }
                else if (stripeEvent.Type == Events.InvoicePaymentActionRequired)
                {
                }
                else if (stripeEvent.Type == Events.InvoicePaymentFailed)
                {
                }
                else if (stripeEvent.Type == Events.InvoicePaymentSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceSent)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceUpcoming)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceUpdated)
                {
                }
                else if (stripeEvent.Type == Events.InvoiceVoided)
                {
                }
                else if (stripeEvent.Type == Events.IssuingAuthorizationCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingAuthorizationUpdated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingCardCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingCardUpdated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingCardholderCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingCardholderUpdated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingDisputeClosed)
                {
                }
                else if (stripeEvent.Type == Events.IssuingDisputeCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingDisputeFundsReinstated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingDisputeSubmitted)
                {
                }
                else if (stripeEvent.Type == Events.IssuingDisputeUpdated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingTokenCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingTokenUpdated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingTransactionCreated)
                {
                }
                else if (stripeEvent.Type == Events.IssuingTransactionUpdated)
                {
                }
                else if (stripeEvent.Type == Events.MandateUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentAmountCapturableUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentCanceled)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentCreated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentPartiallyFunded)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentProcessing)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentRequiresAction)
                {
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.PaymentLinkCreated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentLinkUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                }
                else if (stripeEvent.Type == Events.PaymentMethodAutomaticallyUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PaymentMethodDetached)
                {
                }
                else if (stripeEvent.Type == Events.PaymentMethodUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PayoutCanceled)
                {
                }
                else if (stripeEvent.Type == Events.PayoutCreated)
                {
                }
                else if (stripeEvent.Type == Events.PayoutFailed)
                {
                }
                else if (stripeEvent.Type == Events.PayoutPaid)
                {
                }
                else if (stripeEvent.Type == Events.PayoutReconciliationCompleted)
                {
                }
                else if (stripeEvent.Type == Events.PayoutUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PersonCreated)
                {
                }
                else if (stripeEvent.Type == Events.PersonDeleted)
                {
                }
                else if (stripeEvent.Type == Events.PersonUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PlanCreated)
                {
                }
                else if (stripeEvent.Type == Events.PlanDeleted)
                {
                }
                else if (stripeEvent.Type == Events.PlanUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PriceCreated)
                {
                }
                else if (stripeEvent.Type == Events.PriceDeleted)
                {
                }
                else if (stripeEvent.Type == Events.PriceUpdated)
                {
                }
                else if (stripeEvent.Type == Events.ProductCreated)
                {
                }
                else if (stripeEvent.Type == Events.ProductDeleted)
                {
                }
                else if (stripeEvent.Type == Events.ProductUpdated)
                {
                }
                else if (stripeEvent.Type == Events.PromotionCodeCreated)
                {
                }
                else if (stripeEvent.Type == Events.PromotionCodeUpdated)
                {
                }
                else if (stripeEvent.Type == Events.QuoteAccepted)
                {
                }
                else if (stripeEvent.Type == Events.QuoteCanceled)
                {
                }
                else if (stripeEvent.Type == Events.QuoteCreated)
                {
                }
                else if (stripeEvent.Type == Events.QuoteFinalized)
                {
                }
                else if (stripeEvent.Type == Events.RadarEarlyFraudWarningCreated)
                {
                }
                else if (stripeEvent.Type == Events.RadarEarlyFraudWarningUpdated)
                {
                }
                else if (stripeEvent.Type == Events.RefundCreated)
                {
                }
                else if (stripeEvent.Type == Events.RefundUpdated)
                {
                }
                else if (stripeEvent.Type == Events.ReportingReportRunFailed)
                {
                }
                else if (stripeEvent.Type == Events.ReportingReportRunSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.ReviewClosed)
                {
                }
                else if (stripeEvent.Type == Events.ReviewOpened)
                {
                }
                else if (stripeEvent.Type == Events.SetupIntentCanceled)
                {
                }
                else if (stripeEvent.Type == Events.SetupIntentCreated)
                {
                }
                else if (stripeEvent.Type == Events.SetupIntentRequiresAction)
                {
                }
                else if (stripeEvent.Type == Events.SetupIntentSetupFailed)
                {
                }
                else if (stripeEvent.Type == Events.SetupIntentSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.SigmaScheduledQueryRunCreated)
                {
                }
                else if (stripeEvent.Type == Events.SourceCanceled)
                {
                }
                else if (stripeEvent.Type == Events.SourceChargeable)
                {
                }
                else if (stripeEvent.Type == Events.SourceFailed)
                {
                }
                else if (stripeEvent.Type == Events.SourceMandateNotification)
                {
                }
                else if (stripeEvent.Type == Events.SourceRefundAttributesRequired)
                {
                }
                else if (stripeEvent.Type == Events.SourceTransactionCreated)
                {
                }
                else if (stripeEvent.Type == Events.SourceTransactionUpdated)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleAborted)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleCanceled)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleCompleted)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleCreated)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleExpiring)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleReleased)
                {
                }
                else if (stripeEvent.Type == Events.SubscriptionScheduleUpdated)
                {
                }
                else if (stripeEvent.Type == Events.TaxSettingsUpdated)
                {
                }
                else if (stripeEvent.Type == Events.TaxRateCreated)
                {
                }
                else if (stripeEvent.Type == Events.TaxRateUpdated)
                {
                }
                else if (stripeEvent.Type == Events.TerminalReaderActionFailed)
                {
                }
                else if (stripeEvent.Type == Events.TerminalReaderActionSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.TestHelpersTestClockAdvancing)
                {
                }
                else if (stripeEvent.Type == Events.TestHelpersTestClockCreated)
                {
                }
                else if (stripeEvent.Type == Events.TestHelpersTestClockDeleted)
                {
                }
                else if (stripeEvent.Type == Events.TestHelpersTestClockInternalFailure)
                {
                }
                else if (stripeEvent.Type == Events.TestHelpersTestClockReady)
                {
                }
                else if (stripeEvent.Type == Events.TopupCanceled)
                {
                }
                else if (stripeEvent.Type == Events.TopupCreated)
                {
                }
                else if (stripeEvent.Type == Events.TopupFailed)
                {
                }
                else if (stripeEvent.Type == Events.TopupReversed)
                {
                }
                else if (stripeEvent.Type == Events.TopupSucceeded)
                {
                }
                else if (stripeEvent.Type == Events.TransferCreated)
                {
                }
                else if (stripeEvent.Type == Events.TransferReversed)
                {
                }
                else if (stripeEvent.Type == Events.TransferUpdated)
                {
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
