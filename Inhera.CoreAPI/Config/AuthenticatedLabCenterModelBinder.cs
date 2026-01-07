using Inhera.Shared.Database.SQL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inhera.CoreAPI.Config
{
    public class AuthenticatedLabCenterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Get the lab center from HttpContext.Items (set by LabCenterApiKeyAuthAttribute)
            var labCenter = bindingContext.HttpContext.Items["LabCenter"] as LabCenterEntity;

            if (labCenter == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(labCenter);
            return Task.CompletedTask;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class AuthenticatedLabCenterAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.Custom;
    }

    public class AuthenticatedLabCenterModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Check if the parameter has the AuthenticatedLabCenter attribute
            if (context.Metadata.ModelType == typeof(LabCenterEntity) &&
                context.BindingInfo.BindingSource == BindingSource.Custom)
            {
                return new AuthenticatedLabCenterModelBinder();
            }

            return null;
        }
    }
}
