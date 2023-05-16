using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace Core.Networking
{
    public class HttpPreFlightRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse response = null)
        {
            var httpRequest = request as HttpRequest;
            var httpResponse = response as HttpResponse ?? new HttpResponse();
            var path = Application.persistentDataPath + "/network-cache" + httpRequest!.Uri.LocalPath + "/data.bin";

            httpResponse.CacheHit = true;
            httpResponse.Success = true;
            if (!File.Exists(path))
            {
                httpResponse.CacheHit = false;
                return httpResponse;
            }

            var data = await File.ReadAllTextAsync(path);
            httpResponse.Data = data;

            return httpResponse;
        }
    }
}