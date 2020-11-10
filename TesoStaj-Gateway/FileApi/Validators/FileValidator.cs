using FileApi.Http;
using FileApi.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FileApi.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .OnFailure(x => throw new HttpBadRequest());
            
        }
    }
}