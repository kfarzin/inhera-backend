using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Config
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LabCenterApiKeyAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Get headers
            var apiKey = context.HttpContext.Request.Headers["X-Api-Key"].FirstOrDefault();
            var labCenterId = context.HttpContext.Request.Headers["X-Lab-Center-Id"].FirstOrDefault();

            // Validate headers exist
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(labCenterId))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Missing required headers: X-Api-Key and X-Lab-Center-Id"
                });
                return;
            }

            // Parse Lab Center ID
            if (!Guid.TryParse(labCenterId, out var labCenterGuid))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Invalid X-Lab-Center-Id format"
                });
                return;
            }

            // Get CoreContext from DI
            var dbContext = context.HttpContext.RequestServices.GetService<CoreContext>();
            if (dbContext == null)
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            // Validate API key and lab center
            var labCenter = await dbContext.Set<LabCenterEntity>()
                .FirstOrDefaultAsync(lc => lc.Id == labCenterGuid && lc.AccessKey == apiKey && lc.HasAccess);

            if (labCenter == null)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Invalid API key or Lab Center ID, or access disabled"
                });
                return;
            }

            // Store lab center ID in HttpContext for use in controllers
            context.HttpContext.Items["LabCenterId"] = labCenterGuid;
            context.HttpContext.Items["LabCenter"] = labCenter;
        }
    }
}
