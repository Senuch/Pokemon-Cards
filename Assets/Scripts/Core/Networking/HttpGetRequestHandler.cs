using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Core.Networking
{
    public class HttpGetRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse contextResponse = null)
        {
            HttpRequest webRequest = request as HttpRequest;

            using var www = UnityWebRequest.Get(webRequest!.URL);
            www.SetRequestHeader("Content-Type", "application/json");
            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var response = contextResponse ?? new HttpResponse();

            if (www.result is UnityWebRequest.Result.Success)
            {
                response.Success = true;
                response.Context.Add("DATA", www.downloadHandler.text);
                response.Context.Add("RESPONSE-CODE", www.responseCode);
            }
            else
            {
                response.Success = false;
                response.Context.Add("ERROR", www.error);
                response.Context.Add("RESPONSE-CODE", www.responseCode);
            }

            return response;
        }
    }
}