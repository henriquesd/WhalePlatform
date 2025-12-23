using FluentValidation;
using WP.Core.Messages;

namespace WP.Customer.API.Application.Commands
{
  public class RegisterCustomerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
        {
            public RegisterCustomerValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Customer Id is invalid.");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("Customer name was not informed.");

                RuleFor(c => c.Email)
                    .Must(HasValidEmail)
                    .WithMessage("The email is not valid.");
            }

            protected static bool HasValidEmail(string email)
            {
                return Core.DomainObjects.Email.Validate(email);
            }
        }
    }
}