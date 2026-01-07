using Inhera.CoreAPI.Services;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Profile.Vm;
using Inhera.Shared.VMs.Profile.Vvm;

namespace Inhera.CoreAPI.ResponderServices
{
    public class ProfileResponderService : BaseResponderService
    {
        private readonly ProfileService profileService;
        private readonly PlanService planService;
        private readonly SubscriptionService subscriptionService;
        private readonly AdditionalServiceService additionalServiceService;

        public ProfileResponderService(
            ProfileService profileService,
            PlanService planService,
            SubscriptionService subscriptionService,
            AdditionalServiceService additionalServiceService)
        {
            this.profileService = profileService;
            this.planService = planService;
            this.subscriptionService = subscriptionService;
            this.additionalServiceService = additionalServiceService;
        }

        public async Task<ServiceContainer<UserProfileVm>> GetProfileByEmail(Guid userId)
        {
            try
            {
                var profile = await profileService.GetProfileByUserId(userId);
                if (profile == null)
                {
                    //todo
                    throw new Exception();
                }

                var result = ToUserProfileVm(profile);
                return OkContainer(result);

            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<UserProfileVm>> UpdateOnboardingPersonalDetailsStep(Guid userId, UpdateOnboardingPersonalDetailsStepVvm model)
        {
            try
            {
                var profile = await profileService.UpdateOnboardingPersonalDetailsStep(userId, model);
                if (profile == null)
                {
                    //todo: to log
                    throw new Exception();
                }

                var result = ToUserProfileVm(profile);
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<UserProfileVm>> UpdateOnboardingSubscriptionStep(Guid userId, OnboardingStepIdVvm model)
        {
            try
            {
                var plan = await planService.GetPlanById(model.Id);
                var profile = await profileService.GetProfileByUserId(userId);
                if (plan == null || profile == null)
                {
                    //todo
                    throw new Exception();
                }
                //Does not need to be transactional
                var subscription = await subscriptionService.CreateOrAssignSubscriptionAsync(plan, profile);

                await profileService.MarkProfileAsAdditionalServiceStep(profile);

                var result = ToUserProfileVm(profile);
                return OkContainer(result);

            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }
        

        public async Task<ServiceContainer<UserProfileVm>> UpdateOnboardingAdditionalServicesStep(Guid userId, OnboardingStepIdsVvm model)
        {
            try
            {
                var additionalServices = await additionalServiceService.GetAdditionalServicesByIds(model.Ids);
                var subscription = await profileService.AssignAdditionalServices(userId, additionalServices);
                var profile = await profileService.GetProfileByUserId(userId);


                var result = ToUserProfileVm(profile);
                return OkContainer(result);

            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<UserProfileVm>> UpdateOnboardingLabLocationStep(string email, OnboardingStepIdVvm model)
        {
            try
            {                
                var profile = await profileService.GetProfileByEmail(email);


                var result = ToUserProfileVm(profile);
                return OkContainer(result);

            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<UserProfileVm>> UpdateOnboardingLabCalendarStep(string email, OnboardingStepIdVvm model)
        {
            try
            {
                //TODO: here there should be a call to Lab Calendar API to book the appointment
                var profile = await profileService.GetProfileByEmail(email);


                var result = ToUserProfileVm(profile);
                return OkContainer(result);

            }
            catch (Exception ex)
            {
                return BadContainer<UserProfileVm>(null, LogException(ex));
            }
        }


        private UserProfileVm ToUserProfileVm(UserProfileEntity model)
        {
            var subscription = model.Subscriptions?.FirstOrDefault();
            var result = new UserProfileVm
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CustomerNumber = model.CustomerNumber,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                MobileNumber = model.MobileNumber,
                Email = model.Email,
                IsBoardingCompleted = model.IsBoardingCompleted,
                OnboardingStep = model.OnboardingStep,
                Address = new UserProfileAddressVm
                {
                    Title = model.Address?.Title,
                    FirstName = model.Address?.FirstName ?? string.Empty,
                    LastName = model.Address?.LastName ?? string.Empty,
                    PhoneNumber = model.Address?.PhoneNumber,
                    MobileNumber = model.Address?.MobileNumber,
                    Email = model.Address?.Email,
                    IsDefault = model.Address?.IsDefault,
                    Street = model.Address?.Street,
                    HouseNo = model.Address?.HouseNo,
                    Additional1 = model.Address?.Additional1,
                    Additional2 = model.Address?.Additional2,
                    ZipCode = model.Address?.ZipCode,
                    City = model.Address?.City,
                    Country = model.Address?.Country

                },
                Subscription = new UserProfileSubscriptionVm
                {
                    PlainId = subscription?.PlanId,
                    SubscriptionId = subscription?.Id,
                    Status = subscription?.SubscriptionBookingStatus,
                },
                AdditionalServices = new UserProfileAdditionalServiceVm
                {
                    Ids = subscription?.Services.Select(s => s.AdditionalServiceId).ToList() ?? [],
                }
            };

            return result;
        }
    }
}
