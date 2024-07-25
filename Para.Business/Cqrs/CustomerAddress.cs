using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Business.Cqrs
{
    public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
    public record UpdateCustomerAddressCommand(long CustomerId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerAddressCommand(long CustomerId) : IRequest<ApiResponse>;


    public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
    public record GetCustomerAddressByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;
    public record GetCustomerAddressByParameterQuery(string Country) : IRequest<ApiResponse<List<CustomerAddressResponse>>>;

}
