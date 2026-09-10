using FluentValidation;

namespace Application.Registration
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name is required.")
                .Length(4, 30).WithMessage("Display name must be between 4 and 30 characters long.");
        }
    }
}