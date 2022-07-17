using System.Diagnostics.CodeAnalysis;

namespace Tfl.Client.Configurations
{
    [ExcludeFromCodeCoverage]
    public class TflSettings
    {
        public string ApiBaseUrl { get; set; }

        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string Path { get; set; }
    }
}
