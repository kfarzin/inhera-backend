namespace Inhera.Shared.VMs.Profile.Vm
{
    public class UserProfileVm
    {
        public string? Title { set; get; }
        public string? FirstName { set; get; }
        public string? LastName { set; get; }
        public string? CustomerNumber { set; get; }
        public string PhoneNumber { set; get; } = string.Empty;
        public string MobileNumber { set; get; } = string.Empty;     
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public required string Email { set; get; }        
        public bool IsBoardingCompleted { set; get; }
        public string? OnboardingStep { set; get; }   
        public UserProfileAddressVm? Address { set; get; }
        public UserProfileSubscriptionVm? Subscription { set; get; }
        public UserProfileAdditionalServiceVm? AdditionalServices { set; get; }
    }
}
