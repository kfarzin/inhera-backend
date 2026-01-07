using System.Net;

namespace Inhera.Shared.Services
{
    public record ServiceContainer<T> where T : class
    {
        public T? Data { init; get; }
        public HttpStatusCode StatusCode { init; get; }
        public List<string> Messages { set; get; } = [];
        public string ErrorCode { set; get; } = string.Empty;

        public T? getRaw()
        {
            return Data ?? null;
        }
    }

    public record ServiceContainer
    {
        public object? Data { init; get; }
        public HttpStatusCode StatusCode { init; get; }
        public List<string> Messages { set; get; } = [];
        public string ErrorCode { set; get; } = string.Empty;

        public object? getRaw()
        {
            return Data ?? null;
        }
    }
}
