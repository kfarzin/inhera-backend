using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;
using Inhera.Shared.Util.Common;
using System.Collections.ObjectModel;

namespace Inhera.Shared.Models.Database.SQL.Entities
{
    public class UserProfileEntity : SqlEntity
    {
        public required string AuthId { get; set; }
        public string? Title { set; get; }
        public string? FirstName { set; get; }
        public string? LastName { set; get; }
        public DateTime? DateOfBirth { set; get; }

        [EnumStringValue(typeof(GenderTypes))]
        public string Gender { set; get; } = GenderTypes.Male.ToString();
        public string? CustomerNumber { set; get; }
        public string PhoneNumber { set; get; } = string.Empty;
        public string MobileNumber { set; get; } = string.Empty;        
        public required string Email { set; get; }
        public AddressEntity? Address { set; get; }
        public Guid? AddressId { set; get; }
        
        public bool IsBoardingCompleted { set; get; } = false;

        [EnumStringValue(typeof(OnboardingStepTypes))]
        public string OnboardingStep { set; get; } = OnboardingStepTypes.PersonalDetails.ToString();

        public Collection<SubscriptionEntity> Subscriptions { set; get; } = [];
    }
}