using Core.Entities.Concrete.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class LoginUserValidator : AbstractValidator<UserForLoginDTO>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
    }
}
