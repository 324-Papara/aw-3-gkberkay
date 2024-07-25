using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Schema;

namespace Pa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersDetailController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomersDetailController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<Customers>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<Customers>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        //POST api/<Customers>
        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<Customers>/5
        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        // DELETE api/<Customers>/5
        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerDetailCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        // GET: api/Customers/getByIdentityNumber/{identityNumber}
        [HttpGet("getByIdentityNumber/{identityNumber}")]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> GetByIdentityNumber([FromRoute] string identityNumber)
        {
            var operation = new GetCustomerDetailByParameterQuery(identityNumber);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}