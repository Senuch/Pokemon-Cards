using System.Threading.Tasks;

namespace Core.Networking
{
    public abstract class RequestHandler
    {
        public RequestHandler Next { private get; set; }

        public async Task<IResponse> Handle(IRequest request, IResponse contextResponse = null)
        {
            var response = await Process(request, contextResponse);
            if (!response.Success)
            {
                return response;
            }

            if (Next is not null)
            {
                response = await Next.Handle(request, response);
            }

            return response;
        }

        protected abstract Task<IResponse> Process(IRequest request, IResponse contextResponse = null);
    }
}