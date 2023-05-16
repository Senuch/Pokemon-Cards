using System.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace Core.Networking
{
    public class HttpPostFlightRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse response = null)
        {
            if (response is not HttpResponse { CacheHit: false } httpResponse) return response;
            var data = httpResponse.Data;
            var httpRequest = request as HttpRequest;
            var path = Application.persistentDataPath + "/network-cache" + httpRequest!.Uri.LocalPath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            await File.WriteAllTextAsync(path + "/data.bin",data);

            return response;
        }
    }
}