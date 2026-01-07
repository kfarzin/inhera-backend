namespace Inhera.Shared.VMs.Profile.Vvm
{
    public class UpdateOnboardingPersonalDetailsStepVvm
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MobileNumber { get; set; }
        public required string Gender { get; set; }        
        public required DateTime DateOfBirth { get; set; }
        public required string Country { get; set; }
    }
}
