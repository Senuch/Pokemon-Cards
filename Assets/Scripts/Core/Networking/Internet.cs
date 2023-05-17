using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Event_Manager;
using static Core.Networking.RestClient;

namespace Core.Networking
{
    public static class Internet
    {
        public static bool IsConnected { get; private set; }

        public static void ConnectionStatusChangeEvent(EventManager.EventCallback callback)
        {
            EventManager.Instance.Subscribe("INTERNET_CONNECTION_SERVICE_EVENT", callback);
        }

        public static void UnsubscribeEvent(EventManager.EventCallback callback)
        {
            EventManager.Instance.Unsubscribe("INTERNET_CONNECTION_SERVICE_EVENT", callback);
        }

        public static async void StartConnectionCheckService()
        {
            while (true)
            {
                await Get(
                    "https://www.google.com",
                    new List<(string name, string value)>(),
                    (response =>
                {
                    IsConnected = response is HttpResponse { Success: true };
                    EventManager.Instance.Trigger("INTERNET_CONNECTION_SERVICE_EVENT", IsConnected);
                }), new HttpGetRequestHandler());
                await Task.Delay(5000);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}