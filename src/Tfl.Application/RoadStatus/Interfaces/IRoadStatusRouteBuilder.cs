using System;
using Tfl.Application.CommonInterfaces.RouteBuilders;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Application.RoadStatus.Interfaces
{
    public interface IRoadStatusRouteBuilder : IRouteBuilder
    {
        string Build(Uri baseAddress, RoadStatusRequest request);
    }
}
