using System.Collections.Generic;

namespace Core.Networking
{
    public class HttpResponse : IResponse
    {
        public Dictionary<string, object> Context { get; } = new();
        public bool CacheHit { get; set; }
        public object CacheData { get; set; }
        public bool Success { get; set; }
    }
}