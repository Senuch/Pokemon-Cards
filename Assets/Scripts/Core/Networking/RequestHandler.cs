using System.Threading.Tasks;

namespace Core.Networking
{
    public abstract class RequestHandler
    {
        public RequestHandler Next { private get; set; }

        public async Task<IResponse> Handle(IRequest request, IResponse response = null)
        {
            var result = await Process(request, response);
            if (!result.Success)
            {
                return result;
            }

            if (Next is not null)
            {
                result = await Next.Handle(request, result);
            }

            return result;
        }

        protected abstract Task<IResponse> Process(IRequest request, IResponse response = null);
    }
}