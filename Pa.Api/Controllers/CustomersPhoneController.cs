using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Schema;

namespace Pa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersPhoneController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomersPhoneController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<Customers>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<Customers>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        //POST api/<Customers>
        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<Customers>/5
        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        // DELETE api/<Customers>/5
        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        // GET: api/Customers/getByIdentityNumber/{identityNumber}
        [HttpGet("getByphoneNumber/{phone}")]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetByIdentityNumber([FromRoute] string phone)
        {
            var operation = new GetCustomerPhoneByParameterQuery(phone);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}