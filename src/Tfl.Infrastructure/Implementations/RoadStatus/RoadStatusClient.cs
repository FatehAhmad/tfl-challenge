using Tfl.Domain.RoadStatus.Models.RequestModels.TflApiRequest;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusClient
    {
        private readonly IHttpClientFactory clientFactory;

        public RoadStatusClient(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async ValueTask<string> GetResponse(TflRoadStatusRequest request, CancellationToken cancellationToken)
        {
            var client = clientFactory.CreateClient("tfl");

            var requestUri = $"{client.BaseAddress}{request.Path}{request.Id}";

            var response = await client.GetAsync(requestUri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                ShortCircuitRequest(request.Id, response);
            }
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        private void ShortCircuitRequest(string id, HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"{id} is not a valid road");
            }
            else
            {
                Console.WriteLine($"Request ended with HttpStatusCode: {response.StatusCode}");
            }
            Environment.Exit(1);
        }
    }
}
