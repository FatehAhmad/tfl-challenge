using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Tfl.Domain.RoadStatus.Models.ResponseModels.TflApiResponse;
using Tfl.Infrastructure.Implementations.RoadStatus;

namespace Tfl.Infrastructure.Tests.RoadStatus
{
    [TestFixture]
    public class RoadStatusMapperTests
    {
        private IEnumerable<TflRoadStatusResponse> source;
        private RoadStatusMapper mapper;

        [SetUp]
        public void SetUp()
        {
            source = new List<TflRoadStatusResponse> { new TflRoadStatusResponse { Id = "a1" }, new TflRoadStatusResponse { Id = "a2" } };

            mapper = new RoadStatusMapper();
        }

        [Test, Category("Unit")]
        public void When_Has_Valid_Valid_Data_Should_Map()
        {
            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(source.Count(), result.Count());
            Assert.AreEqual(source.FirstOrDefault().Id, result.FirstOrDefault().Id);
        }

        [Test, Category("Unit")]
        public void When_Source_Data_Is_Empty_Should_Return_Empty_List()
        {
            // Arrange
            source = new List<TflRoadStatusResponse>();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(source.Count(), 0);
        }
    }
}
