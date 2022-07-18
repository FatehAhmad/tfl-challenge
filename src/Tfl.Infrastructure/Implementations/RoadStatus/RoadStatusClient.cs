using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IRoadStatusRouteBuilder routeBuilder;

        public RoadStatusClient(IHttpClientFactory clientFactory,
            IRoadStatusRouteBuilder routeBuilder)
        {
            this.clientFactory = clientFactory;
            this.routeBuilder = routeBuilder;
        }

        public async ValueTask<string> GetResponse(RoadStatusRequest request, CancellationToken cancellationToken)
        {
            var client = clientFactory.CreateClient("tfl");

            var requestUri = routeBuilder.Build(client.BaseAddress, request);

            var response = await client.GetAsync(requestUri, cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                ShortCircuitRequest(request.Id, response, responseString);
            }
            return responseString;
        }

        private void ShortCircuitRequest(string id, HttpResponseMessage response, string responseString)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"{id} is not a valid road");
            }
            else
            {
                Console.WriteLine($"Request ended with HttpStatusCode: {response.StatusCode}");
            }
            Console.WriteLine($"Details: {responseString}");
            Environment.Exit(1);
        }
    }
}
