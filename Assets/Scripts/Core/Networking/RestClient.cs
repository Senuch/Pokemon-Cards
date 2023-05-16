using System;
using System.Threading.Tasks;

namespace Core.Networking
{
    public static class RestClient
    {
        public static async Task Get(string url, Action<IResponse> onCompleted,params RequestHandler[] handlers)
        {
            var request = new HttpRequest(url);
            switch (handlers.Length)
            {
                case 0:
                    return;
                case > 1:
                {
                    var currentHandler = handlers[0];
                    for (int i = 1; i < handlers.Length; i++)
                    {
                        currentHandler.Next = handlers[i];
                        currentHandler = handlers[i];
                    }

                    break;
                }
            }

            var response = await handlers[0].Handle(request);
            onCompleted?.Invoke(response);
        }
    }
}