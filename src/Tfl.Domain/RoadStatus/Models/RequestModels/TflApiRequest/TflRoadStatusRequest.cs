using System.Diagnostics.CodeAnalysis;

namespace Tfl.Domain.RoadStatus.Models.RequestModels.TflApiRequest
{
    [ExcludeFromCodeCoverage]
    public record TflRoadStatusRequest
    {
        public string Id { get; set; }

        public string Path { get; set; }
    }
}
