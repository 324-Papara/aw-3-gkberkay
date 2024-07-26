using Dapper;
using Microsoft.Extensions.Configuration;
using Para.Data.Domain;
using System.Data;
using System.Data.SqlClient;

public class CustomerRepository
{
    private readonly IConfiguration _configuration;

    public CustomerRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Customer> GetCustomersWithDetails()
    {
        using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection")))
        {
            var sql = @"
                SELECT 
                    c.*, 
                    ca.*, 
                    cp.*, 
                    cd.* 
                FROM 
                    Customer c
                LEFT JOIN 
                    CustomerAddress ca ON c.Id = ca.CustomerId
                LEFT JOIN 
                    CustomerPhone cp ON c.Id = cp.CustomerId
                LEFT JOIN 
                    CustomerDetail cd ON c.Id = cd.CustomerId";

            var customerDictionary = new Dictionary<long, Customer>();

            var customers =  db.Query<Customer, CustomerAddress, CustomerPhone, CustomerDetail, Customer>(
                sql,
                (customer, address, phone, detail) =>
                {
                    if (!customerDictionary.TryGetValue(customer.Id, out var currentCustomer))
                    {
                        currentCustomer = customer;
                        currentCustomer.CustomerAddresses = new List<CustomerAddress>();
                        currentCustomer.CustomerPhones = new List<CustomerPhone>();
                        currentCustomer.CustomerDetail = detail;
                        customerDictionary.Add(currentCustomer.Id, currentCustomer);
                    }

                    if (address != null && !currentCustomer.CustomerAddresses.Any(a => a.Id == address.Id))
                    {
                        currentCustomer.CustomerAddresses.Add(address);
                    }

                    if (phone != null && !currentCustomer.CustomerPhones.Any(p => p.Id == phone.Id))
                    {
                        currentCustomer.CustomerPhones.Add(phone);
                    }

                    return currentCustomer;
                },
                splitOn: "Id,Id,Id")
            .Distinct()
            .ToList();

            return customers;
        }
    }
}
