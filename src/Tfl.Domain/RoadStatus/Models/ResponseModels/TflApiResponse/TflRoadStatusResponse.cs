using System.Diagnostics.CodeAnalysis;

namespace Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse
{
    [ExcludeFromCodeCoverage]
    public record TflRoadStatusResponse
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string StatusSeverity { get; set; }

        public string StatusSeverityDescription { get; set; }

        public string Bounds { get; set; }

        public string Envelope { get; set; }

        public string Url { get; set; }
    }
}
