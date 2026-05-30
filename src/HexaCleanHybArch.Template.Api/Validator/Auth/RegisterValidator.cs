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
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}
