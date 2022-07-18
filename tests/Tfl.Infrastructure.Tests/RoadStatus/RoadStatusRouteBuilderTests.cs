using NUnit.Framework;
using System;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Infrastructure.Implementations.RoadStatus;

namespace Tfl.Infrastructure.Tests.RoadStatus
{
    [TestFixture]

    public class RoadStatusRouteBuilderTests
    {
        private RoadStatusRouteBuilder routeBuilder;
        private Uri uri;
        private RoadStatusRequest request;

        [SetUp]
        public void Init()
        {
            routeBuilder = new RoadStatusRouteBuilder();
            uri = new Uri("https://api.tfl.gov.uk/");
            request = new RoadStatusRequest { ApiKey = "", AppId = "", Id = "a2", Path = "road/" };
        }

        [Test, Category("Unit")]
        public void When_Request_Is_Valid_Return_Url()
        {
            var expected = $"{uri.AbsoluteUri}{request.Path}{request.Id}";

            var result = routeBuilder.Build(uri, request);

            Assert.AreEqual(expected, result);
        }

        [Test, Category("Unit")]
        public void Given_That_Request_Is_Valid_With_Multiple_Road_Ids()
        {
            request.Id = "a2,a3";

            var result = routeBuilder.Build(uri, request);
            var resultRoadIds = result.Split("/")[^1];

            Assert.AreEqual(request.Id, resultRoadIds);
        }

        [Test, Category("Unit")]
        public void Given_That_Request_Is_Valid_With_QueryParameters()
        {
            request.ApiKey = "test";
            request.AppId = "test";

            var expected = $"app_id={request.AppId}&app_key={request.ApiKey}";

            var result = routeBuilder.Build(uri, request);
            var queryString = result.Split("?")[^1];

            Assert.AreEqual(expected, queryString);
        }
    }
}
