using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusDataSource : RoadStatusClient, IRoadStatusDataSource
    {
        public RoadStatusDataSource(IHttpClientFactory clientFactory,
            IRoadStatusRouteBuilder routeBuilder)
            : base(clientFactory, routeBuilder)
        {
        }

        public async ValueTask<IEnumerable<TflRoadStatusResponse>> GetRoadStatus(RoadStatusRequest request, CancellationToken token)
        {
            var json = await GetResponse(request, token);

            return JsonSerializer.Deserialize<IEnumerable<TflRoadStatusResponse>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
