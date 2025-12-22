using FluentValidation.Results;
using MediatR;
using WP.Core.Messages;
using WP.Customer.API.Application.Events;
using WP.Customer.API.Repositories;

namespace WP.Customer.API.Application.Commands
{
     public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterCustomerCommand, ValidationResult>,
        IRequestHandler<AddAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Models.Customer(message.Id, message.Name, message.Email);

            var clienteExistente = await _customerRepository.GetByEmail(customer.Email.EmailAddress);

            if (clienteExistente != null)
            {
                AddError("This email is already in use");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new CustomerRegisteredEvent(message.Id, message.Name, message.Email));

            return await PersistData(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AddAddressCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var address = new Models.Address(message.Street, message.Number, message.Complement, message.PostalCode, message.City, message.Province, message.ClientId);
            _customerRepository.AddAddress(address);

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}