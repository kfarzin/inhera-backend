namespace Inhera.Shared.VMs.Payment
{
    public class SubscriptionReviewVm
    {
        public required PaymentProfileVm Profile { get; set; }
        public required PaymentPlanVm SelectedPlan { get; set; }
        public required List<PaymentAdditionalServiceVm> SelectedAdditionalServices { get; set; }
        public required PaymentPricingVm Pricing { get; set; }
    }

    public class PaymentProfileVm
    {
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string CustomerNumber { get; set; }
        public required string PhoneNumber { get; set; }
        public required string MobileNumber { get; set; }
        public required string Gender { get; set; }
        public required string DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required bool IsBoardingCompleted { get; set; }
        public required string OnboardingStep { get; set; }
        public required string Country { get; set; }
        public required PaymentAddressVm Address { get; set; }
    }

    public class PaymentAddressVm
    {
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string MobileNumber { get; set; }
        public required string Email { get; set; }
        public required bool IsDefault { get; set; }
        public required string Street { get; set; }
        public required string HouseNo { get; set; }
        public required string Additional1 { get; set; }
        public required string Additional2 { get; set; }
        public required string ZipCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }

    public class PaymentPlanVm
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string FeaturesSummary { get; set; }
        public required string Currency { get; set; }
        public required int PriceInCents { get; set; }
        public required string BillingCycle { get; set; }
        public required string ApplicableCountry { get; set; }
        public required string CreatedAt { get; set; }
        public required string UpdatedAt { get; set; }
        public required bool IsActive { get; set; }
    }

    public class PaymentAdditionalServiceVm
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string FeaturesSummary { get; set; }
        public required string Currency { get; set; }
        public required int PriceInCents { get; set; }
        public required string ApplicableCountry { get; set; }
        public required string CreatedAt { get; set; }
        public required string UpdatedAt { get; set; }
        public required bool IsActive { get; set; }
    }

    public class PaymentPricingVm
    {
        public required int PlanPrice { get; set; }
        public required int AdditionalServicesPrice { get; set; }
        public required int Subtotal { get; set; }
        public required int Tax { get; set; }
        public required int TaxRate { get; set; }
        public required int Total { get; set; }
        public required string Currency { get; set; }
    }
}
