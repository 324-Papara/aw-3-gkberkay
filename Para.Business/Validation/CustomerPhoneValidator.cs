using FluentValidation;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Data.Validator
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required.")
                .MaximumLength(3).WithMessage("Country code cannot be longer than 5 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d+$").WithMessage("Phone number must be numeric.")
                .MaximumLength(10).WithMessage("Phone number cannot be longer than 10 characters.");

            RuleFor(x => x.IsDefault)
                .NotNull().WithMessage("IsDefault field is required.");
        }
    }
}
