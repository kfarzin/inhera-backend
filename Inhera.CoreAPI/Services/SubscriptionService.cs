using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class SubscriptionService : SqlService<SubscriptionEntity, CoreContext>
    {
        public SubscriptionService(SqlRepository<SubscriptionEntity, CoreContext> repository) : base(repository)
        {
        }

        public async Task<SubscriptionEntity?> GetCurrentPendingSubscription(UserProfileEntity userProfile)
        {
            var currentPendingSubscription = await _repository.GetRawRepository()
                .Include(e => e.Plan)
                .FirstOrDefaultAsync(e => e.UserProfileId == userProfile.Id
                && e.SubscriptionBookingStatus == SubscriptionBookingStatusTypes.Pending.ToString());

            return currentPendingSubscription;
        }

        public async Task<SubscriptionEntity?> CreateOrAssignSubscriptionAsync(PlanEntity plan, UserProfileEntity userProfile)
        {
            var currentSubscription = await _repository.GetRawRepository()
                .FirstOrDefaultAsync(e => e.UserProfileId == userProfile.Id
                && e.SubscriptionBookingStatus == SubscriptionBookingStatusTypes.Pending.ToString());

            if (currentSubscription == null)
            {
                currentSubscription = new SubscriptionEntity
                {
                    PlanId = plan.Id,
                    UserProfileId = userProfile.Id,
                    SelectedAtUtc = DateTimeOffset.UtcNow,                    
                    SubscriptionBookingStatus = SubscriptionBookingStatusTypes.Pending.ToString()
                };
                await _repository.Add(currentSubscription);
            }
            else
            {
                currentSubscription.PlanId = plan.Id;
                currentSubscription.SelectedAtUtc = DateTimeOffset.UtcNow;
            }

            await _repository.SaveChangesAsync();
            return currentSubscription;
        }
    }
}

