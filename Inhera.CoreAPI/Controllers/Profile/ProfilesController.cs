using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.Profile.Vm;
using Inhera.Shared.VMs.Profile.Vvm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.Profile
{
    [Authorize]
    [ApiController]
    public class ProfilesController : CoreAPIController
    {
        private readonly ProfileResponderService profileResponderService;
        private readonly ConsoleLogger logger;

        public ProfilesController(
            ProfileResponderService profileResponderService,
            ConsoleLogger consoleLogger
            )
        {
            logger = consoleLogger;
            this.profileResponderService = profileResponderService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> GetProfileByEmail()
        {
            return CreatedContainer(await profileResponderService.GetProfileByEmail(GetAuthUserId()));
        }

        //personal-details
        [HttpPut("onboarding/steps/personal-details")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingPersonalDetailsStep(
            UpdateOnboardingPersonalDetailsStepVvm model)
        {
            return AcceptedContainer(await profileResponderService.UpdateOnboardingPersonalDetailsStep(GetAuthUserId(), model));
        }

        //subscription
        [HttpPut("onboarding/steps/subscriptions")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingSubscriptionStep(
            OnboardingStepIdVvm model)
        {
            return AcceptedContainer(await profileResponderService.UpdateOnboardingSubscriptionStep(GetAuthUserId(), model));
        }

        //additional-services
        [HttpPut("onboarding/steps/additional-services")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingAdditionalServicesStep(
            OnboardingStepIdsVvm model)
        {
            return AcceptedContainer(await profileResponderService.UpdateOnboardingAdditionalServicesStep(GetAuthUserId(), model));
        }


        //location     return of this call is a calendar route
        [HttpPut("onboarding/steps/lab-location")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingLabLocationStep(
            OnboardingStepIdVvm model)
        {
            return AcceptedContainer(await profileResponderService.UpdateOnboardingLabLocationStep(GetAuthUserEmail(), model));
        }

        //appointment return of this call is either an address or questionnaire route       
        [HttpPut("onboarding/steps/calendar")]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingLabCalendarStep(
            OnboardingStepIdsVvm model)
        {
            //return AcceptedContainer(await profileResponderService.UpdateOnboardingLabCalendarStep(GetAuthUserEmail(), model));
            return AcceptedContainer(await profileResponderService.UpdateOnboardingLabCalendarStep(GetAuthUserEmail(), null));
        }

        //appointment return of this call is questionnaire route
        //[HttpPut("onboarding/steps/address")]
        //public async Task<ActionResult<StandardContainer<UserProfileVm>>> UpdateOnboardingLabLocationStep(
        //    OnboardingStepIdsVvm model)
        //{
        //    return AcceptedContainer(await profileResponderService.UpdateOnboardingLabLocationStep(GetAuthUserEmail(), model));
        //}        
    }
}
