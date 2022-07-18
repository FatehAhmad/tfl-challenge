using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Client.Controllers;
using Tfl.Client.Validators;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;

namespace Tfl.Client.Tests.Controllers
{
    [TestFixture]
    public class RoadStatusControllerTests
    {
        private Mock<IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>>> roadStatusHandlerMock;
        private Mock<RoadStatusRequestValidator> roadStatusRequestValidatorMock;
        private RoadStatusController controller;
        private IEnumerable<RoadStatusResponse> response;
        private RoadStatusRequest request;

        [SetUp]
        public void SetUp()
        {
            roadStatusHandlerMock = new Mock<IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>>>();
            roadStatusRequestValidatorMock = new Mock<RoadStatusRequestValidator>();
            controller = new RoadStatusController(roadStatusHandlerMock.Object, roadStatusRequestValidatorMock.Object);
            response = new List<RoadStatusResponse>();
            request = new RoadStatusRequest { Id = "a1" };

            roadStatusRequestValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<RoadStatusRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        }

        [Test, Category("Unit")]
        public async Task Get_Should_Returns_Ok_With_List_Of_Roads()
        {
            // Arrange
            roadStatusHandlerMock.Setup(x => x.HandleAsync(It.IsAny<RoadStatusRequest>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(response);

            // Act
            var result = await controller.Get(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test, Category("Unit")]
        public async Task Get_Should_Call_Handler()
        {
            // Act
            var result = await controller.Get(request, CancellationToken.None);

            // Assert
            roadStatusHandlerMock.Verify(x => x.HandleAsync(It.IsAny<RoadStatusRequest>(), CancellationToken.None));
            Assert.That(result, Is.Not.Null);
        }
    }
}
