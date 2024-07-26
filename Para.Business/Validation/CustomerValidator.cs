using FluentValidation;
using Para.Data.Context;
using Para.Schema;

namespace Para.Data.Validator
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        private readonly ParaDbContext _dbContext;
        public CustomerValidator(ParaDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Please specify an author")
                .Length(11).WithMessage("IdentityNumber must be exactly 11 characters long")
                .Must(BeUniqueIdentityNumber).WithMessage("Identity number '{PropertyValue}' is already taken."); ;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.").Must(BeUniqueEmail).WithMessage("Email address '{PropertyValue}' is already taken.")
                .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");


            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.");
        }

        private bool BeUniqueEmail(string email)
        {
            return !_dbContext.Customers.Any(c => c.Email == email);
        }

        private bool BeUniqueIdentityNumber(string identityNumber)
        {
            return !_dbContext.Customers.Any(c => c.IdentityNumber == identityNumber);
        }
    }
}