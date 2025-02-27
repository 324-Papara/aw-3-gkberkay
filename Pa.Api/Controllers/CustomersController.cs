﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Schema;

namespace Pa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<Customers>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<Customers>/5
        [HttpGet("{Id}")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute] long Id)
        {
            var operation = new GetCustomerByIdQuery(Id);
            var result = await mediator.Send(operation);
            return result;
        }

        //POST api/<Customers>
        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
        {
            var validationOperation = new ValidateCustomerCommand(value);
            var operation = new CreateCustomerCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<Customers>/5
        [HttpPut("{Id}")]
        public async Task<ApiResponse> Put(long Id, [FromBody] CustomerRequest value)
        {
            var operation = new UpdateCustomerCommand(Id, value);
            var result = await mediator.Send(operation);
            return result;
        }

        // DELETE api/<Customers>/5
        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(long Id)
        {
            var operation = new DeleteCustomerCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }

        // GET: api/Customers/getByIdentityNumber/{identityNumber}
        [HttpGet("getByIdentityNumber/{identityNumber}")]
        public async Task<ActionResult<ApiResponse<List<CustomerResponse>>>> GetByIdentityNumber([FromRoute] string identityNumber)
        {
            var operation = new GetCustomerByParameterQuery(identityNumber);
            var result = await mediator.Send(operation);
            return Ok(result);
        }
    }
}