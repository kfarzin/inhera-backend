using Inhera.Shared.Services;
using System.Net;

namespace Inhera.Shared.Services
{
    public class ServiceResult
    {        
        public ServiceContainer<N> CreatedContainer<N>(N data, string errorCode = "") where N : class
        {
            return GetContainer(data, HttpStatusCode.Created, errorCode, []);
        }

        public ServiceContainer<N> OkContainer<N>(N data, string errorCode = "") where N : class
        {
            return GetContainer(data, HttpStatusCode.OK, errorCode, []);
        }

        public ServiceContainer<N> AcceptedContainer<N>(N data, string errorCode = "") where N : class
        {
            return GetContainer(data, HttpStatusCode.Accepted, errorCode, []);
        }

        public ServiceContainer AcceptedContainer(string errorCode = "")
        {
            return GetContainer(HttpStatusCode.Accepted, errorCode, []);
        }

        public ServiceContainer<N> BadContainer<N>(N? data = null, string errorCode = "") where N : class
        {
            return GetContainer(data, HttpStatusCode.BadRequest, errorCode, []);
        }

        //empty containers
        public ServiceContainer BadEmptyContainer(string errorCode = "")
        {
            return GetEmptyContainer(HttpStatusCode.BadRequest, errorCode, []);
        }

        private ServiceContainer<N> GetContainer<N>(N? data, HttpStatusCode statusCode, string errorCode, List<string> messages) where N : class
        {
            return new ServiceContainer<N>()
            {
                Data = data,
                StatusCode = statusCode,
                Messages = messages,
                ErrorCode = errorCode,
            };
        }

        private ServiceContainer GetContainer(HttpStatusCode statusCode, string errorCode, List<string> messages)
        {
            return new ServiceContainer()
            {                
                StatusCode = statusCode,
                Messages = messages,
                ErrorCode = errorCode,
            };
        }

        private ServiceContainer GetEmptyContainer(HttpStatusCode statusCode, string errorCode, List<string> messages)
        {
            return new ServiceContainer()
            {
                Data = null,
                StatusCode = statusCode,
                Messages = messages,
                ErrorCode= errorCode,
            };
        }
    }
}
