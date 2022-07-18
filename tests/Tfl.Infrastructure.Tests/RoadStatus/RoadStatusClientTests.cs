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
    public class RoadStatusClientTests
    {
        private Mock<IHttpClientFactory> httpClientFactoryMock;
        private Mock<IRoadStatusRouteBuilder> roadStatusRouteBuilderMock;
        private Mock<HttpMessageHandler> messageHandlerMock;
        private CancellationToken token;
        private HttpClient httpClient;
        private RoadStatusClient roadStatusClient;

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
            roadStatusRouteBuilderMock.Setup(x => x.Build(It.IsAny<Uri>(), It.IsAny<RoadStatusRequest>()))
                .Returns(httpClient.BaseAddress.AbsoluteUri);

            roadStatusClient = new RoadStatusClient(httpClientFactoryMock.Object, roadStatusRouteBuilderMock.Object);

            //Arrange
            messageHandlerMock.Protected()
                              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                              .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent("[{ \"id\": \"1\" }]")
                              });
        }

        [Test, Category("Unit")]
        public async Task GetResponse_Should_Return_Ok()
        {
            // Act
            var result = await roadStatusClient.GetResponse(new RoadStatusRequest(), token);

            // Assert
            Assert.NotNull(result);
            Assert.That(200, Is.EqualTo((int)HttpStatusCode.OK));
        }
    }
}
