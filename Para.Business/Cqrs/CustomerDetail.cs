﻿using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Business.Cqrs
{
    public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
    public record UpdateCustomerDetailCommand(long CustomerId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerDetailCommand(long CustomerId) : IRequest<ApiResponse>;
    public record ValidateCustomerDetailCommand(CustomerDetailRequest CustomerDetailRequest) : IRequest<ApiResponse>;


    public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
    public record GetCustomerDetailByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;
    public record GetCustomerDetailByParameterQuery(string FatherName) : IRequest<ApiResponse<List<CustomerDetailResponse>>>;

}
