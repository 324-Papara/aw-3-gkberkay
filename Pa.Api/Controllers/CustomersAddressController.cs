using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Schema;

namespace Pa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersAddressController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomersAddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<Customers>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<Customers>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        //POST api/<Customers>
        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<Customers>/5
        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        // DELETE api/<Customers>/5
        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerAddressCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        // GET: api/Customers/getByIdentityNumber/{identityNumber}
        [HttpGet("getByCountry/{country}")]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> GetByIdentityNumber([FromRoute] string country)
        {
            var operation = new GetCustomerAddressByParameterQuery(country);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}