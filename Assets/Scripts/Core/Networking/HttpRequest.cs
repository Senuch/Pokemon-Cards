using System;

namespace Core.Networking
{
    public class HttpRequest : IRequest
    {
        public Uri Uri { get; private set; }

        public HttpRequest(string url)
        {
            Uri = new Uri(url);
        }
    }
}