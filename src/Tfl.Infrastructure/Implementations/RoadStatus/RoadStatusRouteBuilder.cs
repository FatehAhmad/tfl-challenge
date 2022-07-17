using Flurl;
using System;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Infrastructure.Implementations.RoadStatus
{
    public class RoadStatusRouteBuilder : IRoadStatusRouteBuilder
    {
        public string Build(Uri baseAddress, RoadStatusRequest request)
        {
            var url = new Url(baseAddress) { Path = $"{request.Path}{request.Id}" };

            if (!string.IsNullOrWhiteSpace(request.AppId))
            {
                url.SetQueryParam("app_id", request.AppId);
            }

            if (!string.IsNullOrWhiteSpace(request.ApiKey))
            {
                url.SetQueryParam("app_key", request.ApiKey);
            }

            return url.ToUri().AbsoluteUri;
        }
    }
}
