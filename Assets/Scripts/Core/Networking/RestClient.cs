using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Networking
{
    public static class RestClient
    {
        public static async void Get(
            string url,
            Action<IResponse> onCompleted = null,
            params RequestHandler[] handlers)
        {
            await Get(
                url,
                new List<(string name, string value)>{("Content-Type", "application/json")},
                onCompleted,
                handlers);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task Get(
            string url,
            List<(string name, string value)> headers,
            Action<IResponse> onCompleted = null,
            params RequestHandler[] handlers)
        {
            var request = new HttpRequest(url, headers);
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