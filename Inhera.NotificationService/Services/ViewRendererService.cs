using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.NotificationService.Services
{
    public class ViewRendererService
    {
        public interface IViewRenderService
        {
            Task<string> RenderToStringAsync(string viewName, object model, bool isPartial = false);
        }

        public class ViewRenderService : IViewRenderService
        {
            private readonly IRazorViewEngine _razorViewEngine;
            private readonly ITempDataProvider _tempDataProvider;
            private readonly IServiceProvider _serviceProvider;

            public ViewRenderService(IRazorViewEngine razorViewEngine,
                ITempDataProvider tempDataProvider,
                IServiceProvider serviceProvider)
            {
                _razorViewEngine = razorViewEngine;
                _tempDataProvider = tempDataProvider;
                _serviceProvider = serviceProvider;
            }

            public async Task<string> RenderToStringAsync(string viewName, object model, bool isPartial = false)
            {
                var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

                using (var sw = new StringWriter())
                {
                    var viewResult = _razorViewEngine.FindView(actionContext, viewName, !isPartial);

                    if (viewResult?.View == null)
                        throw new ArgumentException($"{viewName} does not match any available view");

                    var viewDictionary =
                        new ViewDataDictionary(new EmptyModelMetadataProvider(),
                            new ModelStateDictionary());
                    viewDictionary.Model = model;                        

                    var viewContext = new ViewContext(
                        actionContext,
                        viewResult.View,
                        viewDictionary,
                        new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                        sw,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);
                    return sw.ToString();
                }
            }
        }
    }
}
