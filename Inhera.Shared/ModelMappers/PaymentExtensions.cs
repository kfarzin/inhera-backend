using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.VMs.Payment;

namespace Inhera.Shared.ModelMappers
{
    public static class PaymentExtensions
    {
        public static PaymentVm ToPaymentVm(this PaymentEntity entity)
        {
            return new PaymentVm
            {
                Id = entity.Id,
                StripePaymentIntentId = entity.StripePaymentIntentId,
                Amount = entity.Amount,
                Currency = entity.Currency,
                Status = entity.Status,
                SubscriptionId = entity.SubscriptionId,
                UserId = entity.UserId,
                FailureReason = entity.FailureReason,
                PaidAt = entity.PaidAt,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
