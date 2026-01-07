using Inhera.Authentication.Models;
using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Profile.Vvm;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class ProfileService : SqlService<UserProfileEntity, CoreContext>
    {
        private readonly UserManager<AuthUser> userManager;
        public ProfileService(SqlRepository<UserProfileEntity, CoreContext> repository, UserManager<AuthUser> userManager) : base(repository)
        {
            this.userManager = userManager;
        }

        public async Task<UserProfileEntity?> GetProfileByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null!;
            }

            var profile = await _repository.GetRawRepository()                    
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.AuthId.Equals(user.Id));
            if (profile != null)
            {
                return profile;
            }

            return null!;
        }

        public async Task<UserProfileEntity?> GetProfileByUserId(Guid id)
        {
            var profile = await _repository.GetRawRepository()                
                .Include(e => e.Address)
                .Include(e => e.Subscriptions.Where(x => x.IsActive
                    && x.SubscriptionBookingStatus == SubscriptionBookingStatusTypes.Pending.ToString()))
                    .ThenInclude(e => e.Services)
                .ThenInclude (e => e.AdditionalService)                    
                .FirstOrDefaultAsync(e => e.Id == id);

            if (profile != null)
            {
                return profile;
            }

            return null!;
        }

        public async Task<UserProfileEntity?> UpdateOnboardingPersonalDetailsStep(Guid userId, UpdateOnboardingPersonalDetailsStepVvm model)
        {
            var profile = await GetProfileByUserId(userId);
            if (profile == null)
            {
                return null!;
            }            

            var utcDateTime = DateTime.SpecifyKind(model.DateOfBirth, DateTimeKind.Utc);

            if (profile != null)
            {
                profile.FirstName = model.FirstName;
                profile.LastName = model.LastName;
                profile.Gender = model.Gender;
                profile.DateOfBirth = utcDateTime;
                profile.MobileNumber = model.MobileNumber;
                profile.OnboardingStep = OnboardingStepTypes.Subscription.ToString();

                if(profile.Address != null)
                {
                    profile.Address.Country = model.Country;
                }
                else
                {
                    var address = new AddressEntity
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Country = model.Country,
                    };
                    profile.Address = address;
                }

                await _repository.SaveChangesAsync();
                return profile;
            }

            return null!;
        }

        public async Task<SubscriptionEntity?> AssignAdditionalServices(Guid userId, List<AdditionalServiceEntity> additionalServices)
        {
            var profile = await _repository.GetRawRepository()
                .Include(e => e.Subscriptions.Where(e => e.SubscriptionBookingStatus == SubscriptionBookingStatusTypes.Pending.ToString()))
                    .ThenInclude(e => e.Services)
                .FirstOrDefaultAsync(e => e.Id == userId);

            var pendingSubscription = profile!.Subscriptions.FirstOrDefault();
            
            foreach (var subscriptionService in pendingSubscription.Services)
            {
                _repository.GetContext().Entry(subscriptionService).State = EntityState.Deleted;
            }
            pendingSubscription.Services.Clear();


            foreach (var additionalService in additionalServices)
            {
                var subscriptionService = new SubscriptionAdditionalServiceEntity
                {
                    AdditionalServiceId = additionalService.Id,
                    SubscriptionId = pendingSubscription.Id
                };
                pendingSubscription.Services.Add(subscriptionService);
            }

            await MarkProfileAsAdditionalServiceStep(profile);

            await _repository.SaveChangesAsync();
            return pendingSubscription;
        }


        public async Task<UserProfileEntity?> MarkProfileAsAdditionalServiceStep(UserProfileEntity model)
        {
            model.OnboardingStep = OnboardingStepTypes.AdditionalServices.ToString();
            await _repository.SaveChangesAsync();

            return model;
        }

        public async Task<UserProfileEntity?> MarkProfileAsPaymentStep(UserProfileEntity model)
        {
            model.OnboardingStep = OnboardingStepTypes.Payment.ToString();
            await _repository.SaveChangesAsync();

            return model;
        }

    }
}