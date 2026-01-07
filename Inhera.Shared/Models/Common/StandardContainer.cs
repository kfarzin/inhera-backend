using System.Net;

namespace Inhera.Shared.Models.Common
{
    public record StandardContainer<T> where T : class
    {
        public required T Data { init; get; }        
        public int Version { init;  get; }
        public HttpStatusCode StatusCode { init;  get; }
        public DateTimeOffset TimeStamp { set; get; }
        public List<string> Messages { set; get; } = [];
        public string ErrorCode {  set; get; } = string.Empty;

        public void UpdateTimeStamp(DateTimeOffset timeStamp)
        {
            TimeStamp = timeStamp;
        }

        public void AddMessages(List<string> messages)
        {
            Messages = messages;
        }
    }
}
