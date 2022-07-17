using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Mappers;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusHandler : IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>>
    {
        private readonly IRoadStatusDataSource dataSource;
        private readonly IMapper<IEnumerable<TflRoadStatusResponse>, IEnumerable<RoadStatusResponse>> mapper;

        public RoadStatusHandler(IRoadStatusDataSource dataSource,
            IMapper<IEnumerable<TflRoadStatusResponse>, IEnumerable<RoadStatusResponse>> mapper)
        {
            this.dataSource = dataSource;
            this.mapper = mapper;
        }

        public async ValueTask<IEnumerable<RoadStatusResponse>> HandleAsync(RoadStatusRequest request, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            var response = await dataSource.GetRoadStatus(request, token);

            return mapper.Map(response);
        }
    }
}
