using FluentValidation;
using jwtwithLayer.JwtNoIdentity.Core.DTOs;

namespace jwtwithLayer.JwtNoIdentity.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() {
            RuleFor(x => x.FullName)
                           .NotEmpty().WithMessage("FullName is required")
                           .MinimumLength(3).WithMessage("FullName must be at least 3 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required");

        }
    }
 
}
