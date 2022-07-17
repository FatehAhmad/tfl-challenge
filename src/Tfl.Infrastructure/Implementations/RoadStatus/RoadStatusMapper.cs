using System.Collections.Generic;
using Tfl.Application.CommonInterfaces.Mappers;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusMapper : IMapper<IEnumerable<TflRoadStatusResponse>, IEnumerable<RoadStatusResponse>>
    {
        public IEnumerable<RoadStatusResponse> Map(IEnumerable<TflRoadStatusResponse> source)
        {
            var result = new List<RoadStatusResponse>();

            foreach (var roadStatus in source)
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
