using FluentValidation;

namespace Pa.Api.Validator
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1000, 10000).WithMessage("Id must be between 1000 and 10000.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a book name.").Length(5, 30).WithMessage("Name must be between 5 and 30 characters.");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Please specify a author").Length(5, 30).WithMessage("Author must be between 5 and 30 characters.");
            RuleFor(x => x.PageCount).InclusiveBetween(50, 200).WithMessage("Page count must be between 50 and 200.");
            RuleFor(x => x.Year).InclusiveBetween(1900, 2024).WithMessage("Year must be between 1900 and 2024.");
        }
    }
}
