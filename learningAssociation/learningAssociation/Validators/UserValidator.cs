using FluentValidation;
using learningAssociation.Models;


namespace learningAssociation.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("name is required").Length(2, 40).WithMessage("name must between between 2 to 40");
            RuleFor(u => u.Email).NotEmpty().WithMessage("email not be null").EmailAddress().WithMessage("Invalid email formate");
            RuleFor(u => u.Password).NotNull().WithMessage("please enter email").MinimumLength(8)
                .WithMessage("Password must be at least 8 characters").MaximumLength(20).
                 WithMessage("Password cannot exceed 20 characters").Matches(@"[A-Z]")
                .WithMessage("PASSWORD MUST CANTAIN ONE UPPER CASE LATTER ").
                 Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter").
                 Matches(@"\d").WithMessage("password must contain at least one number").
                 Matches(@"[\!\@\$\%\^\&|*\(\)\+\=]").WithMessage("Password must contain at least one special character");



        }
    }
}
