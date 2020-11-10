using System;
using FluentValidation;
using UserApi.Http;
using UserApi.Models;

namespace UserApi.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .Length(5, 30)
                .NotEmpty()
                .OnFailure(x => throw new HttpBadRequest());
            RuleFor(x => x.Password)
                .NotEmpty()
                .OnFailure(x => throw new HttpBadRequest());
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .OnFailure(x => throw new HttpBadRequest());
            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(HaveValidRole)
                .OnFailure(x => throw new HttpBadRequest());
        }

        private bool HaveValidRole(string role)
        {
            return Enum.IsDefined(typeof(Roles), role);
        }
    }
}