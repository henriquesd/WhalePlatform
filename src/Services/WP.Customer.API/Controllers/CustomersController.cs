using Microsoft.AspNetCore.Mvc;
using WP.Core.Mediator;
using WP.Customer.API.Application.Commands;
using WP.Customer.API.Repositories;
using WP.WebAPI.Core.Controllers;
using WP.WebAPI.Core.User;

namespace WP.Customer.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public CustomersController(ICustomerRepository customerRepository, IMediatorHandler mediator, IAspNetUser user)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("customer/address")]
        public async Task<IActionResult> GetAddress()
        {
            var endereco = await _customerRepository.GetAddressById(_user.GetUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("customer/address")]
        public async Task<IActionResult> AddAddress(AddAddressCommand addressCommand)
        {
            addressCommand.ClientId = _user.GetUserId();
            return CustomResponse(await _mediator.SendCommand(addressCommand));
        }
    }
}