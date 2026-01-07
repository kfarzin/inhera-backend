namespace Inhera.Shared.VMs.Profile.Vm
{
    public class UserProfileAddressVm
    {
        public string? Title { set; get; }
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public string? PhoneNumber { set; get; }
        public string? MobileNumber { set; get; }
        public string? Email { set; get; }
        public bool? IsDefault { set; get; } = true;        

        public string? Street { set; get; }
        public string? HouseNo { set; get; }
        public string? Additional1 { set; get; }
        public string? Additional2 { set; get; }
        public string? ZipCode { set; get; }
        public string? City { set; get; }
        public string? Country { set; get; }
    }
}
