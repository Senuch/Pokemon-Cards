namespace Core.Networking
{
    public class HttpRequest : IRequest
    {
        public string URL { get; private set; }

        public HttpRequest(string url)
        {
            URL = url;
        }
    }
}