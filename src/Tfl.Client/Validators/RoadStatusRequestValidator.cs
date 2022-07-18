using FluentValidation;
using Tfl.Domain.RoadStatus.Models.RequestModels;

namespace Tfl.Client.Validators
{
    public class RoadStatusRequestValidator : AbstractValidator<RoadStatusRequest>
    {
        public RoadStatusRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id(s) cannot be null or empty");
            RuleFor(x => x.Id).Must(x => x == null || !x.Contains(" ")).WithMessage("Id(s) should not contain white spaces. Please replace spaces with %20, eg: blackwall%20tunnel");
        }
    }
}
