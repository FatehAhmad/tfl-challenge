using System.Diagnostics.CodeAnalysis;

namespace Tfl.Domain.RoadStatus.Models.ResponseModels
{
    [ExcludeFromCodeCoverage]
    public record RoadStatusResponse
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string StatusSeverity { get; set; }

        public string statusSeverityDescription { get; set; }
    }
}
