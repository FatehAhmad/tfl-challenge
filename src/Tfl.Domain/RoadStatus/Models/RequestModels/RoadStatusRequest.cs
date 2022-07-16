using System.Diagnostics.CodeAnalysis;

namespace Tfl.Domain.RoadStatus.Models.RequestModels
{
    [ExcludeFromCodeCoverage]
    public record RoadStatusRequest
    {
        public string Id { get; set; }
    }
}
