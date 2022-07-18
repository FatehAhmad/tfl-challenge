using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Mappers;
using Tfl.Application.RoadStatus.Interfaces;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;
using Tfl.Infrastructure.Implementations.RoadStatus;

namespace Tfl.Infrastructure.Tests.RoadStatus
{
    [TestFixture]
    public class RoadStatusHandlerTests
    {
        private Mock<IRoadStatusDataSource> dataSource;
        private IMapper<IEnumerable<TflRoadStatusResponse>, IEnumerable<RoadStatusResponse>> mapper;
        private CancellationToken token;
        private RoadStatusHandler roadStatusHandler;
        private RoadStatusRequest request;
        private IEnumerable<TflRoadStatusResponse> response;

        [SetUp]
        public void Init()
        {
            dataSource = new Mock<IRoadStatusDataSource>();
            mapper = new RoadStatusMapper();
            roadStatusHandler = new RoadStatusHandler(dataSource.Object, mapper);
            token = CancellationToken.None;
            request = new RoadStatusRequest { Id = "a1" };
            response = new List<TflRoadStatusResponse> { new TflRoadStatusResponse { Id = "a1", DisplayName = "A2" } };
        }

        [Test, Category("Unit")]
        public async Task When_Result_Has_Rows_Should_Return()
        {
            // Arrange
            dataSource.Setup(x => x.GetRoadStatus(It.IsAny<RoadStatusRequest>(),
                                                       It.IsAny<CancellationToken>()))
                               .ReturnsAsync(response);

            // Act
            var result = await roadStatusHandler.HandleAsync(request, token);

            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}
