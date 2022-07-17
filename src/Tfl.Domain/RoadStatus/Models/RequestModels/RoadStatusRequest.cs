using System.Diagnostics.CodeAnalysis;

namespace Tfl.Domain.RoadStatus.Models.RequestModels
{
    [ExcludeFromCodeCoverage]
    public record RoadStatusRequest
    {
        public string Id { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }

        public string Path { get; set; }
    }
}
