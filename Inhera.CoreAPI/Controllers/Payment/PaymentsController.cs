using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.Payment
{
    [Authorize]
    [ApiController]
    public class PaymentsController : CoreAPIController
    {
        private readonly PaymentResponderService paymentResponderService;
        private readonly ConsoleLogger logger;

        public PaymentsController(
            PaymentResponderService paymentResponderService,
            ConsoleLogger consoleLogger)
        {
            this.paymentResponderService = paymentResponderService;
            this.logger = consoleLogger;
        }

        //currently these endpoints are not needed to be exposed

        //[HttpPost("payment-intent")]
        //public async Task<ActionResult<StandardContainer<PaymentIntentVm>>> CreatePaymentIntent(
        //    [FromBody] CreatePaymentIntentVvm model)
        //{
        //    return OkContainer(await paymentResponderService.CreatePaymentIntent(GetAuthUserId(), model));
        //}

        //[HttpGet("payments/{id}")]
        //public async Task<ActionResult<StandardContainer<PaymentVm>>> GetPayment(Guid id)
        //{
        //    return OkContainer(await paymentResponderService.GetPaymentById(id));
        //}

        //[HttpGet("my-payments")]
        //public async Task<ActionResult<StandardContainer<List<PaymentVm>>>> GetMyPayments()
        //{
        //    return OkContainer(await paymentResponderService.GetUserPayments(GetAuthUserId()));
        //}

        [HttpPost()]
        public async Task<ActionResult<StandardContainer<PaymentIntentVm>>> CreatePaymentIntent()
        {
            return OkContainer(await paymentResponderService.CreatePaymentIntentForActivePlan(GetAuthUserId()));
        }

        [HttpGet("subscription-review")]
        public async Task<ActionResult<StandardContainer<SubscriptionReviewVm>>> GetSubscriptionReview()
        {
            return OkContainer(await paymentResponderService.GetSubscriptionReview(GetAuthUserId()));
        }

        [HttpPut("process-validation")]
        public async Task<ActionResult<StandardContainer<PaymentStatusVm>>> ValidatePayment(
            [FromBody] PaymentValidationVvm model)
        {
            return OkContainer(await paymentResponderService.ValidatePayment(GetAuthUserId(), model));
        }
    }
}
