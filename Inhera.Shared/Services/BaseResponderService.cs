using Serilog;

namespace Inhera.Shared.Services
{
    public class BaseResponderService : ServiceResult
    {
        protected string LogException(Exception ex)
        {
            Log.Error(ex, "Exception");
            return string.Empty;
        }
        protected ServiceContainer<T> GenerateErrorResponse<T>(Exception ex) where T : class
        {
            Log.Error(ex, "Exception");           
            return BadContainer<T>(null);
        }

        protected List<T> EmptyList<T>()
        {
            return new List<T>();
        }
        
        protected bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        //protected void RaiseExceptionIf<EX>(bool condition) where EX : BaseInternalCodeException
        //{
        //    if (condition)
        //    {
        //        throw Activator.CreateInstance<EX>();
        //    }
        //}        
    }
}
