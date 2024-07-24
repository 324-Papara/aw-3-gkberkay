using FluentValidation;
using Para.Data.Domain;

namespace Para.Data.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Please specify an author")
                .Length(11).WithMessage("IdentityNumber must be exactly 11 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.")
                .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");

            //RuleFor(x => x.CustomerNumber)
            //    .NotEmpty().WithMessage("Customer number is required.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.");

            RuleFor(x => x.CustomerDetail)
                .SetValidator(new CustomerDetailValidator());

            RuleForEach(x => x.CustomerAddresses)
                .SetValidator(new CustomerAddressValidator());

            RuleForEach(x => x.CustomerPhones)
                .SetValidator(new CustomerPhoneValidator());

        }
    }
}