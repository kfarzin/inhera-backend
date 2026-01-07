using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;

namespace Inhera.Shared.Database.Seeds
{    
    public static class AdditionalServiceSeeds
    {
        public static List<AdditionalServiceEntity> GetSeeds()
        {
            List<AdditionalServiceEntity> additionalServices = new List<AdditionalServiceEntity>();

            AdditionalServiceEntity additionalServiceGermanyAppointment = new AdditionalServiceEntity
            {
                Name = "Doctor Appointment",
                Description = "Core plan for individual users.",                
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_DOCTOR_APPOINTMENT_DE",
                Type = AdditionalServiceTypes.InPerson.ToString(),
                PriceInCents = 999,                
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            additionalServices.Add(additionalServiceGermanyAppointment);

            AdditionalServiceEntity additionalServiceGermanyCollection = new AdditionalServiceEntity
            {
                Name = "CollectionService",
                Description = "Core plan for individual users.",
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_COLLECTION_DE",
                Type = AdditionalServiceTypes.Collection.ToString(),
                PriceInCents = 222,
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            additionalServices.Add(additionalServiceGermanyCollection);

            AdditionalServiceEntity additionalServiceEnglandAppointment = new AdditionalServiceEntity
            {
                Name = "Doctor Appointment",
                Description = "Core plan for individual users.",
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_DOCTOR_APPOINTMENT_GB",
                Type = AdditionalServiceTypes.InPerson.ToString(),
                PriceInCents = 999,
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            additionalServices.Add(additionalServiceEnglandAppointment);

            AdditionalServiceEntity additionalServiceEnglandCollection = new AdditionalServiceEntity
            {
                Name = "CollectionService",
                Description = "Core plan for individual users.",
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_COLLECTION_GB",
                Type = AdditionalServiceTypes.Collection.ToString(),
                PriceInCents = 888,
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            additionalServices.Add(additionalServiceEnglandCollection);


            return additionalServices;
        }
    }
}
