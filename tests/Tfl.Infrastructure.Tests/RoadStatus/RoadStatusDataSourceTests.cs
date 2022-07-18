using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Infrastructure.Implementations.RoadStatus;

namespace Tfl.Infrastructure.Tests.RoadStatus
{
    [TestFixture]
    public class RoadStatusDataSourceTests
    {
        private IRoadStatusDataSource dataSource;
        private Mock<IHttpClientFactory> httpClientFactoryMock;
        private Mock<IRoadStatusRouteBuilder> roadStatusRouteBuilderMock;
        private Mock<HttpMessageHandler> messageHandlerMock;
        private CancellationToken token;
        private HttpClient httpClient;
        private RoadStatusRequest request;

        [SetUp]
        public void Init()
        {
            httpClientFactoryMock = new Mock<IHttpClientFactory>();
            messageHandlerMock = new Mock<HttpMessageHandler>();
            roadStatusRouteBuilderMock = new Mock<IRoadStatusRouteBuilder>();
            token = CancellationToken.None;

            httpClient = new HttpClient(messageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.tfl.gov.uk/")
            };

            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            dataSource = new RoadStatusDataSource(httpClientFactoryMock.Object, roadStatusRouteBuilderMock.Object);

            // Arrange
            messageHandlerMock.Protected()
                              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                              .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent("[{ \"id\": \"1\" }]")
                              });

            request = new RoadStatusRequest { Id = "a1" };
        }


        [Test, Category("Unit")]
        public async Task GetRoadStatus_Should_Return_Ok()
        {
            // Act
            var result = await dataSource.GetRoadStatus(request, token);

            // Assert
            Assert.NotNull(result);
            Assert.That(200, Is.EqualTo((int)HttpStatusCode.OK));
        }
    }
}
