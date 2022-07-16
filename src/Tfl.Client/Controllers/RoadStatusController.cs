using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Client.Validators;

namespace Tfl.Client.Controllers
{
    public class RoadStatusController
    {
        private readonly IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>> RoadStatusHandler;
        private readonly RoadStatusRequestValidator RoadStatusRequestValidator;

        public RoadStatusController(IHandler<RoadStatusRequest, IEnumerable<RoadStatusResponse>> roadStatusHandler,
            RoadStatusRequestValidator roadStatusRequestValidator)
        {
            this.RoadStatusHandler = roadStatusHandler;
            this.RoadStatusRequestValidator = roadStatusRequestValidator;
        }

        /// <summary>
        /// Reads status of the road with the given Id from Tfl Api
        /// </summary>
        /// <param name="roadStatusRequest">Road Status Request<see cref="RoadStatusRequest"/></param>
        /// <returns>RoadStatusResponse<see cref="Task<RoadStatusResponse>"/></returns>
        public async Task<IEnumerable<RoadStatusResponse>> Get(RoadStatusRequest roadStatusRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = await RoadStatusRequestValidator.ValidateAsync(roadStatusRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.Errors.FirstOrDefault());
                Environment.Exit(1);
            }

            return await RoadStatusHandler.HandleAsync(roadStatusRequest, cancellationToken);
        }
    }
}
