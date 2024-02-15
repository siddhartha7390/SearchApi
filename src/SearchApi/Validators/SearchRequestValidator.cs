using Application.Core.Models;
using FluentValidation;

public class SearchRequestValidator : AbstractValidator<SearchRequest>
{
    public SearchRequestValidator()
    {
        RuleFor(x => x.AuthorName).MaximumLength(50);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PublicationYear).GreaterThan(0).WithMessage("PublicationYear must be a positive value.");
    }
}
