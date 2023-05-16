using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace Core.Networking
{
    public class HttpPreFlightRequestHandler : RequestHandler
    {
        protected override async Task<IResponse> Process(IRequest request, IResponse contextResponse = null)
        {
            var httpRequest = request as HttpRequest;
            var response = new HttpResponse();
            string pokemonName = GetPokemonName(httpRequest!.URL);
            string path = Application.persistentDataPath + "/pokemon/" + pokemonName;

            response.CacheHit = true;
            response.Success = true;
            if (!File.Exists(path))
            {
                response.CacheHit = false;
                return response;
            }

            string data = await File.ReadAllTextAsync(path);
            response.CacheData = data;

            return response;
        }

        private string GetPokemonName(string url)
        {
            var request = url.Split('/');
            return request[^1];
        }
    }
}