using FluentValidation;
using Para.Data.Domain;

namespace Para.Data.Validator
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetail>
    {
        public CustomerDetailValidator()
        {
            RuleFor(x => x.FatherName)
                .NotEmpty().WithMessage("Father name is required.")
                .MaximumLength(50).WithMessage("Father name cannot be longer than 50 characters.");

            RuleFor(x => x.MotherName)
                .NotEmpty().WithMessage("Mother name is required.")
                .MaximumLength(50).WithMessage("Mother name cannot be longer than 50 characters.");

            RuleFor(x => x.MountlyIncome)
                .NotEmpty().WithMessage("Monthly income is required.")
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Monthly income must be a valid monetary amount.")
                .MaximumLength(50).WithMessage("MountlyIncome  cannot be longer than 50 characters.");

            RuleFor(x => x.Occupation)
                .NotEmpty().WithMessage("Occupation is required.")
                .MaximumLength(50).WithMessage("Occupation cannot be longer than 50 characters.");

            RuleFor(x => x.EducationStatus)
               .NotEmpty().WithMessage("Education status is required.")
               .MaximumLength(50).WithMessage("Education status cannot be longer than 50 characters.");
        }
    }
}
