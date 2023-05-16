using System.Threading.Tasks;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Core.Networking
{
    public class HttpPostFlightRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse contextResponse = null)
        {
            if (contextResponse is HttpResponse { CacheHit: false })
            {
                string data = contextResponse.Context["DATA"] as string;
                var httpRequest = request as HttpRequest;
                string pokemonName = GetPokemonName(httpRequest!.URL);
                string path = Application.persistentDataPath + "/pokemon/";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                await File.WriteAllTextAsync(path + pokemonName,data, CancellationToken.None);
            }

            return contextResponse;
        }

        private string GetPokemonName(string url)
        {
            var request = url.Split('/');
            return request[^1];
        }
    }
}