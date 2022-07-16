using System.Threading;
using System.Threading.Tasks;

namespace Tfl.Application.CommonInterfaces.Handlers
{
    public interface IHandler<in TRequest, TResponse>
    {
        public ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken token = default);
    }
}
