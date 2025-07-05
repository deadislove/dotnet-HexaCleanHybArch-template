using FluentValidation;
using HexaCleanHybArch.Template.Api.Validator.Bases;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;

namespace HexaCleanHybArch.Template.Api.Validator
{
    public class RegisterValidator: BaseValidator<RegisterCoreRequest>
    {
        protected override void ConfigureRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
