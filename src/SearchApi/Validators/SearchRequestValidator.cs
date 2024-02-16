using Application.Core.Models;
using FluentValidation;

public class SearchRequestValidator : AbstractValidator<SearchRequest>
{
    public SearchRequestValidator()
    {
        RuleFor(x => x.AuthorName).MaximumLength(255);
        RuleFor(x => x.Title).MaximumLength(100);
        RuleFor(x => x.PublicationYear).GreaterThanOrEqualTo(0).WithMessage("PublicationYear must be a positive value.");
    }
}
