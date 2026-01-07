using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;

namespace Inhera.Shared.Database.Seeds
{
    public static class PlanSeeds
    {
        public static List<PlanEntity> GetSeeds()
        {
            List<PlanEntity> plans = new List<PlanEntity>();
            
            PlanEntity corePlanMonthlyGermany = new PlanEntity
            {
                Name = "Inhera Core",
                Description = "Core plan for individual users.",
                BillingCycle = BillingCycleTypes.Monthly.ToString(),
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_CORE_MONTHLY_DE",
                PriceInCents = 999,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(corePlanMonthlyGermany);

            PlanEntity lightPlanMonthlyGermany = new PlanEntity
            {
                Name = "Inhera Light",
                Description = "Light plan for individual users.",
                BillingCycle = BillingCycleTypes.Monthly.ToString(),
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_LIGHT_MONTHLY_DE",
                PriceInCents = 100,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(lightPlanMonthlyGermany);


            PlanEntity corePlanYearlyGermany = new PlanEntity
            {
                Name = "Inhera Core",
                Description = "Core plan for individual users.",
                BillingCycle = BillingCycleTypes.Yearly.ToString(),
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_CORE_YEARLY_DE",
                PriceInCents = 2999,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(corePlanYearlyGermany);

            PlanEntity lightPlanYearlyGermany = new PlanEntity
            {
                Name = "Inhera Light",
                Description = "Light plan for individual users.",
                BillingCycle = BillingCycleTypes.Yearly.ToString(),
                Currency = CurrencyTypes.EUR.ToString(),
                ApplicableCountry = PlanCountryTypes.DE.ToString(),
                Code = "INHERA_LIGHT_YEARLY_DE",
                PriceInCents = 2100,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(lightPlanYearlyGermany);

            ////////
            PlanEntity corePlanMonthlyEngland = new PlanEntity
            {
                Name = "Inhera Core",
                Description = "Core plan for individual users.",
                BillingCycle = BillingCycleTypes.Monthly.ToString(),
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_CORE_MONTHLY_GB",
                PriceInCents = 999,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            }
            ;
            plans.Add(corePlanMonthlyEngland);

            PlanEntity lightPlanMonthlyEngland = new PlanEntity
            {
                Name = "Inhera Light",
                Description = "Light plan for individual users.",
                BillingCycle = BillingCycleTypes.Monthly.ToString(),
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_LIGHT_MONTHLY_GB",
                PriceInCents = 100,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(lightPlanMonthlyEngland);


            PlanEntity corePlanYearlyEngland = new PlanEntity
            {
                Name = "Inhera Core",
                Description = "Core plan for individual users.",
                BillingCycle = BillingCycleTypes.Yearly.ToString(),
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_CORE_YEARLY_GB",
                PriceInCents = 2999,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(corePlanYearlyEngland);

            PlanEntity lightPlanYearlyEngland = new PlanEntity
            {
                Name = "Inhera Light",
                Description = "Light plan for individual users.",
                BillingCycle = BillingCycleTypes.Yearly.ToString(),
                Currency = CurrencyTypes.GBP.ToString(),
                ApplicableCountry = PlanCountryTypes.GB.ToString(),
                Code = "INHERA_LIGHT_YEARLY_GB",
                PriceInCents = 2100,
                //comma separated list of features
                FeaturesSummary = "Basic Reports,Health Insights,Ancestry Analysis",
                IsActive = true
            };
            plans.Add(lightPlanYearlyEngland);


            return plans;
        }
    }
}
