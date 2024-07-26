using Microsoft.AspNetCore.Mvc;
using Para.Data.Domain;

[Route("api/[controller]")]
[ApiController]
public class CustomerReportController : ControllerBase
{
    private readonly CustomerRepository _customerRepository;

    public CustomerReportController(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        var customers = _customerRepository.GetCustomersWithDetails();
        return Ok(customers);
    }
}
