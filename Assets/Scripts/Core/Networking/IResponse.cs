using System.Collections.Generic;

namespace Core.Networking
{
    public interface IResponse
    {
        public Dictionary<string, object> Context { get; }
        public bool Success { get; set; }
    }
}