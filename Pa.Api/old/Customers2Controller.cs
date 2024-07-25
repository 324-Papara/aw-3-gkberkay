//using Microsoft.AspNetCore.Mvc;
//using Para.Base.Response;
//using Para.Data.Domain;
//using Para.Data.UnitOfWork;
//using Para.Data.Validator;

//namespace Pa.Api.old
//{
//    [NonController]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomersController : ControllerBase
//    {
//        private readonly IUnitOfWork unitOfWork;
//        public CustomersController(IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//        }

//        // GET: api/<Customers>
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var entityList = await unitOfWork.CustomerRepository.GetAll();
//            if (entityList is null)
//            {
//                return NotFound(new ApiResponse("Customers not found."));
//            }

//            return Ok(new ApiResponse<List<Customer>>(entityList));
//        }

//        // GET api/<Customers>/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(long customerId)
//        {
//            var entityList = await unitOfWork.CustomerRepository.GetById(customerId);
//            if (entityList is null)
//            {
//                return NotFound(new ApiResponse("Customer not found."));
//            }
//            return Ok(new ApiResponse<Customer>(entityList));
//        }

//        // GET: api/Customers/getByIdentityNumber/{identityNumber}
//        [HttpGet("getByIdentityNumber/{identityNumber}")]
//        public async Task<IActionResult> GetByIdentityNumber(string identityNumber)
//        {
//            var customer = await unitOfWork.CustomerRepository.GetAllWithIncludeAsync(x => x.IdentityNumber == identityNumber, p => p.CustomerDetail, p => p.CustomerAddresses, p => p.CustomerPhones);
//            if (customer is null)
//            {
//                return NotFound(new ApiResponse("Customer not found."));
//            }

//            return Ok(new ApiResponse<Customer?>(customer.FirstOrDefault()));
//        }

//        // POST api/<Customers>
//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] Customer value)
//        {

//            CustomerValidator validator = new CustomerValidator();

//            var result = validator.Validate(value);
//            if (!result.IsValid)
//            {
//                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
//                return BadRequest(new ApiResponse(string.Join(", ", errorMessages)));
//            }
//            await unitOfWork.CustomerRepository.Insert(value);
//            await unitOfWork.CompleteWithTransaction();
//            return Ok(new ApiResponse<Customer>(value));
//        }

//        // PUT api/<Customers>/5
//        [HttpPut("{customerId}")]
//        public IActionResult Put(long customerId, [FromBody] Customer value)
//        {
//            CustomerValidator validator = new CustomerValidator();
//            var result = validator.Validate(value);
//            if (!result.IsValid)
//            {
//                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
//                return BadRequest(new ApiResponse(string.Join(", ", errorMessages)));
//            }
//            var existingCustomer = unitOfWork.CustomerRepository.GetById(customerId);
//            if (existingCustomer == null)
//            {
//                return NotFound(new ApiResponse("Customer not found."));
//            }
//            unitOfWork.CustomerRepository.Update(value);
//            unitOfWork.Complete();
//            return Ok(new ApiResponse<Customer>(value)); ;

//        }

//        // DELETE api/<Customers>/5
//        [HttpDelete("{customerId}")]
//        public async void Delete(long customerId)
//        {
//            await unitOfWork.CustomerRepository.Delete(customerId);
//            await unitOfWork.Complete();

//        }
//    }
//}