using FluentValidation;
using WP.Core.Messages;

namespace WP.Customer.API.Application.Commands
{
     public class AddAddressCommand : Command
    {
        public Guid ClientId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public AddAddressCommand()
        {
        }

        public AddAddressCommand(
            Guid clientId,
            string street,
            string number,
            string complement,
            string postalCode,
            string city,
            string province)
        {
            AggregateId = clientId;
            ClientId = clientId;
            Street = street;
            Number = number;
            Complement = complement;
            PostalCode = postalCode;
            City = city;
            Province = province;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AddressValidation : AbstractValidator<AddAddressCommand>
        {
            public AddressValidation()
            {
                RuleFor(c => c.Street)
                    .NotEmpty()
                    .WithMessage("Inform the Street");

                RuleFor(c => c.Number)
                    .NotEmpty()
                    .WithMessage("Inform the Number");

                RuleFor(c => c.PostalCode)
                    .NotEmpty()
                    .WithMessage("Inform the Postal Code");

                RuleFor(c => c.City)
                    .NotEmpty()
                    .WithMessage("Inform the City");

                RuleFor(c => c.Province)
                    .NotEmpty()
                    .WithMessage("Inform the Province");
            }
        }
    }
}