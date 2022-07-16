using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Application.RoadStatus.Handlers
{
    public class RoadStatusHandler : IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>>
    {
        private readonly IRoadStatusDataSource DataSource;

        public RoadStatusHandler(IRoadStatusDataSource dataSource)
        {
            this.DataSource = dataSource;
        }

        public async ValueTask<IEnumerable<RoadStatusResponse>> HandleAsync(RoadStatusRequest request, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            var dataSourceResponse = await DataSource.GetRoadStatus(request, token);

            return Mapper(dataSourceResponse);
        }

        private List<RoadStatusResponse> Mapper(IEnumerable<TflRoadStatusResponse> dataSourceResponse)
        {
            var result = new List<RoadStatusResponse>();

            foreach (var roadStatus in dataSourceResponse)
            {
                result.Add(new RoadStatusResponse
                    {
                        Id = roadStatus.id,
                        DisplayName = roadStatus.displayName,
                        StatusSeverity = roadStatus.statusSeverity,
                        statusSeverityDescription = roadStatus.statusSeverityDescription
                    });
            }
            return result;
        }
    }
}
