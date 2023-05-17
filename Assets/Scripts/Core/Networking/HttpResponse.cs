using System.Collections.Generic;

namespace Core.Networking
{
    public class HttpResponse : IResponse
    {
        public Dictionary<string, object> Context { get; } = new();
        public bool CacheHit { get; set; }
        public bool Success { get; set; }
        public bool Retry => ResponseCode is HttpResponseCodes.InternalServerError or HttpResponseCodes.NoResponse;
        public string Data
        {
            get => Context.TryGetValue("data", out var data) ? (string)data : default;
            set => Context.Add("data", value);
        }
        public string Error
        {
            get => Context.TryGetValue("error", out var data) ? (string)data : default;
            set => Context.Add("error", value);
        }
        public long ResponseCode
        {
            get => Context.TryGetValue("response-code", out var data) ? (long)data : default;
            set => Context.Add("response-code", value);
        }
        public HttpRequest Request
        {
            get => Context.TryGetValue("request", out var data) ? (HttpRequest)data : default;
            set => Context.Add("request", value);
        }
    }
}