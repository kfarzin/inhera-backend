using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.Seeds;
using System;

namespace Inhera.CoreAPI.Config
{
    public static class AppSeeder
    {
        public static void Seed(WebApplication app)
        {
            SeedPlansAndSubscriptions(app);
            SeedAdditionalServices(app);
        }

        private static void SeedPlansAndSubscriptions(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CoreContext>();
                var plans = PlanSeeds.GetSeeds();
                foreach (var plan in plans)
                {
                    var existingPlan = db.Plans.FirstOrDefault(p => p.Code == plan.Code);
                    if (existingPlan == null)
                    {
                        db.Plans.Add(plan);
                    }
                }

                db.SaveChanges();
            }
        }

        private static void SeedAdditionalServices(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CoreContext>();
                var additionalServices = AdditionalServiceSeeds.GetSeeds();
                foreach (var additionalService in additionalServices)
                {
                    var existingAdditionalService = db.AdditionalServices.FirstOrDefault(p => p.Code == additionalService.Code);
                    if (existingAdditionalService == null)
                    {
                        db.AdditionalServices.Add(additionalService);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}