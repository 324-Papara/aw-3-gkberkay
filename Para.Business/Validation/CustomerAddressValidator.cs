using FluentValidation;
using Para.Data.Domain;
using Para.Schema;


namespace Para.Data.Validator
{
    public class CustomerAddressValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot be longer than 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot be longer than 100 characters.");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Address line is required.")
                .MaximumLength(250).WithMessage("Address line cannot be longer than 200 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.")
                .MaximumLength(6).WithMessage("Zip code cannot be longer than 20 characters.");

            RuleFor(x => x.IsDefault)
                .NotNull().WithMessage("IsDefault field is required.");

        }
    }
}
