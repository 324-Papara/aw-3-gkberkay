using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Business.Cqrs
{
    public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record UpdateCustomerPhoneCommand(long CustomerId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerPhoneCommand(long CustomerId) : IRequest<ApiResponse>;
    public record ValidateCustomerPhoneCommand(CustomerPhoneRequest CustomerPhoneRequest) : IRequest<ApiResponse>;



    public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
    public record GetCustomerPhoneByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record GetCustomerPhoneByParameterQuery(string Phone) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;

}
