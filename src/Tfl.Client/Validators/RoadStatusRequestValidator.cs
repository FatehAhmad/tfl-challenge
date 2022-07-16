using FluentValidation;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Client.Validators
{
    public class RoadStatusRequestValidator : AbstractValidator<RoadStatusRequest>
    {
        public RoadStatusRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be null or empty");
        }
    }
}
