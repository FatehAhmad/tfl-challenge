using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.RequestModels.TflApiRequest;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusDataSource : RoadStatusClient, IRoadStatusDataSource
    {
        private readonly string Path = "road/";

        public RoadStatusDataSource(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
        }

        public async ValueTask<IEnumerable<TflRoadStatusResponse>> GetRoadStatus(RoadStatusRequest request, CancellationToken token)
        {
            var json = await GetResponse(new TflRoadStatusRequest { Id = request.Id, Path = Path }, token);

            return JsonSerializer.Deserialize<IEnumerable<TflRoadStatusResponse>>(json);
        }
    }
}
