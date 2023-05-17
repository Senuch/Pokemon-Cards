using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Core.Networking
{
    public class HttpGetRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse response = null)
        {
            var httpRequest = request as HttpRequest;
            if (response is HttpResponse { CacheHit: true } cacheResponse)
            {
                cacheResponse.Request = httpRequest;
                return cacheResponse;
            }

            using var www = UnityWebRequest.Get(httpRequest!.Uri.ToString());
            foreach (var header in httpRequest.Headers)
            {
                www.SetRequestHeader(header.name, header.value);
            }

            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var httpResponse = response as HttpResponse ?? new HttpResponse();
            if (www.result is UnityWebRequest.Result.Success)
            {
                httpResponse.Success = true;
                httpResponse.Data = www.downloadHandler.text;
                httpResponse.ResponseCode = www.responseCode;
            }
            else
            {
                httpResponse.Success = false;
                httpResponse.Error = www.error;
                httpResponse.ResponseCode = www.responseCode;
            }

            httpResponse.Request = httpRequest;
            return httpResponse;
        }
    }
}