using FluentValidation;
using HexaCleanHybArch.Template.Api.Validator.Bases;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;

namespace HexaCleanHybArch.Template.Api.Validator
{
    public class LoginValidator : BaseValidator<LoginCoreRequest>
    {
        protected override void ConfigureRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
