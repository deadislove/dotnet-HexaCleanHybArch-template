using FluentValidation;

namespace HexaCleanHybArch.Template.Api.Validator.Bases
{
    public abstract class BaseValidator<T>: AbstractValidator<T>
    {
        public BaseValidator() {
            ConfigureRules();
        }

        protected abstract void ConfigureRules();
    }
}
