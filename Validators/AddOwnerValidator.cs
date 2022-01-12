using FluentValidation;
using OnlineVetAPI.DomainModels;

namespace OnlineVetAPI.Validators
{
    public class AddOwnerValidator : AbstractValidator<AddNewOwner>
    {
        public AddOwnerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MobileNumber).NotEmpty();
            RuleFor(x => x.OwnerEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
