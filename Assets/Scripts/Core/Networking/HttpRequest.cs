using System;
using System.Collections.Generic;

namespace Core.Networking
{
    public class HttpRequest : IRequest
    {
        public Uri Uri { get; private set; }
        public List<(string name, string value)> Headers { get; private set; }

        public HttpRequest(string url, List<(string, string)> headers)
        {
            Uri = new Uri(url);
            Headers = headers;
        }
    }
}