using FluentValidation;
using LoginApp.Services.Identity.Dtos;

namespace LoginApp.Services.Identity.Validation
{
    public class CredentialsValidator: AbstractValidator<Credentials>
    {
        private const int MaxUserNameLength = 50;
        private const int MaxPasswordLength = 50;

        public CredentialsValidator()
        {
            RuleFor(cred => cred.UserName).NotEmpty().WithMessage("User name cannot be empty");
            RuleFor(cred => cred.Password).NotEmpty().WithMessage("Password name cannot be empty");
            RuleFor(cred => cred.UserName).MaximumLength(MaxUserNameLength).WithMessage("User name is too long");
            RuleFor(cred => cred.Password).MaximumLength(MaxPasswordLength).WithMessage("Password is too long");


        }
    }
}
