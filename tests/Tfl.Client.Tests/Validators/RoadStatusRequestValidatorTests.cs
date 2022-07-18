using NUnit.Framework;
using System.Threading.Tasks;
using Tfl.Client.Validators;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Client.Tests.Validators
{
    [TestFixture]
    public class RoadStatusRequestValidatorTests
    {
        private RoadStatusRequestValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new RoadStatusRequestValidator();
        }

        [TestCase(null, false), Category("Unit")]
        [TestCase("", false), Category("Unit")]
        [TestCase("a1", true), Category("Unit")]
        [TestCase("a 1", false), Category("Unit")]
        public async Task When_Validating_Request_Should_Return_Result(string id, bool expectedResult)
        {
            // Arrange
            var model = new RoadStatusRequest()
            {
                Id = id
            };

            // Act
            var result = await validator.ValidateAsync(model);

            // Assert
            Assert.AreEqual(expectedResult, result.IsValid);
        }
    }
}
