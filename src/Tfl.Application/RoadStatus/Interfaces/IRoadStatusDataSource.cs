using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.DataProviders;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Application.RoadStatus.Interfaces
{
    public interface IRoadStatusDataSource : IDataProvider
    {
        ValueTask<IEnumerable<TflRoadStatusResponse>> GetRoadStatus(RoadStatusRequest request, CancellationToken token);
    }
}

