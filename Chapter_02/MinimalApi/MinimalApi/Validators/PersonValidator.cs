using FluentValidation;
using FluentValidation.Results;
using MinimalApi.Models;
using System.Text.RegularExpressions;

namespace MinimalApi.Validators
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator() { 
            RuleFor(x=>x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                 .Custom((name, context) =>
                 {
                     if (name != null)
                     {
                         Regex rg = new Regex("<.*?>");
                         if (rg.Matches(name).Count() > 0)
                         {
                             context.AddFailure(
                                 new ValidationFailure(
                                     "Name", "The parameter has invalid content")
                                 );
                         }
                     }
                 });

            RuleFor(x => x.Family)
              .NotEmpty()
              .WithMessage("{PropertyName} is required")
              .Matches("^[a-zA-Z]+$")
              .WithMessage("{PropertyName} has invalid content");

            RuleFor(x => x.WebsiteUrl)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

    }
}
}
