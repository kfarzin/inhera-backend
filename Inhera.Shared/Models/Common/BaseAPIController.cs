using Inhera.Shared.Services;
using Inhera.Shared.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace Inhera.Shared.Models.Common
{
    [Route("api/v1/[controller]")]
    public class BaseAPIController : ControllerBase
    {

        const int MaxCheckoutCookieExpirationInDays = 90;
        //todo: fix the cookie refresh, job to refresh the sessions
        const int MaxGenericSessionCookieExpirationInDays = 365 * 1;
        //todo: to be replaced
        int version = 1;

        protected IIdentity? GetAuthUserIdentity()
        {
            return HttpContext.User.Identity;
        }

        protected bool HasExpiredToken()
        {
            var email = GetAuthUserEmail();
            if (email.IsNotEmptyOrNull())
            {
                return false;
            }
            const string HeaderKey = "Authorization";
            Request.Headers.TryGetValue(HeaderKey, out StringValues value);
            var parts = value.FirstOrDefault()?.Split("Bearer ");
            if(parts is not null && parts.Length == 2 && parts[1].IsNotEmptyOrNull())
            {
                return true;
            }
            return false;
        }        

        protected string GetAuthUserEmail()
        {
            var result = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email.ToString());
            return result != null ? result.Value.ToString() : string.Empty;
        }

        protected Guid GetAuthUserId()
        {
            var result = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());            
            if (result == null || string.IsNullOrEmpty(result.Value))
            {
                throw new UnauthorizedAccessException("User ID claim not found");
            }
            
            if (!Guid.TryParse(result.Value, out Guid userId))
            {
                throw new InvalidOperationException($"Invalid User ID format: {result.Value}");
            }
            
            return userId;
        }

        protected ObjectResult CreatedContainer<T>(ServiceContainer<T> model) where T : class
        {
            return GetObjectResult(GetContainer(model.Data!, version, model.StatusCode, model.ErrorCode, []));
        }

        protected ObjectResult OkContainer<T>(ServiceContainer<T> model) where T : class
        {
            return GetObjectResult(GetContainer(model.Data!, version, model.StatusCode, model.ErrorCode, []));
        }

        protected ObjectResult AcceptedContainer<T>(ServiceContainer<T> model) where T : class
        {
            return GetObjectResult(GetContainer(model.Data!, version, model.StatusCode, model.ErrorCode, []));
        }

        protected ObjectResult DeleteContainer<T>(ServiceContainer<T> model) where T : class
        {
            return GetObjectResult(GetContainer(model.Data!, version, model.StatusCode, model.ErrorCode, []));
        }

        protected StandardContainer<T> GetContainer<T>(T data, int version, HttpStatusCode statusCode, string errorCode, List<string> messages) where T : class
        {            
            return new StandardContainer<T>()
            {
                Data = data,                
                Version = version,
                StatusCode = statusCode,
                TimeStamp = DateTimeOffset.UtcNow,
                Messages = messages,
                ErrorCode = errorCode,
            };
        }


        private ObjectResult GetObjectResult<T>(StandardContainer<T> model) where T : class
        {
            return StatusCode((int)model.StatusCode, model);
        }
    }
}
