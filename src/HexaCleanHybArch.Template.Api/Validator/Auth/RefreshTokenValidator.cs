using FluentValidation;
using HexaCleanHybArch.Template.Api.Validator.Bases;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;

namespace HexaCleanHybArch.Template.Api.Validator
{
    public class RefreshTokenValidator : BaseValidator<RefreshTokenCoreRequest>
    {
        protected override void ConfigureRules()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}
