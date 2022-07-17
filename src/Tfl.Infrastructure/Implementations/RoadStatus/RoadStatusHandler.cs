using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Infrastructure.Implementations.RoadStatus
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

            var response = await DataSource.GetRoadStatus(request, token);

            return Mapper(response);
        }

        private List<RoadStatusResponse> Mapper(IEnumerable<TflRoadStatusResponse> response)
        {
            var result = new List<RoadStatusResponse>();

            foreach (var roadStatus in response)
            {
                result.Add(new RoadStatusResponse
                    {
                        Id = roadStatus.Id,
                        DisplayName = roadStatus.DisplayName,
                        StatusSeverity = roadStatus.StatusSeverity,
                        statusSeverityDescription = roadStatus.StatusSeverityDescription
                    });
            }
            return result;
        }
    }
}
