using System.Collections.Generic;

namespace Core.Networking
{
    public class HttpResponse : IResponse
    {
        public Dictionary<string, object> Context { get; } = new();
        public bool ContinueExecution { get; set; }
        public bool Success { get; set; }
    }
}