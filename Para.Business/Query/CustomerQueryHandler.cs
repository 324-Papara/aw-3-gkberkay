﻿using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Query
{
    public class CustomerQueryHandler :
        IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
        IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>,
        IRequestHandler<GetCustomerByParameterQuery, ApiResponse<List<CustomerResponse>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Customer> entityList = await unitOfWork.CustomerRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
            return new ApiResponse<List<CustomerResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            Customer entity = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParameterQuery request, CancellationToken cancellationToken)
        {
            var customers = await unitOfWork.CustomerRepository.GetAllWithIncludeAsync(
                x => x.IdentityNumber == request.IdentityNumber,
                p => p.CustomerDetail,
                p => p.CustomerAddresses,
                p => p.CustomerPhones
            );
            var customer = customers.FirstOrDefault();
            var mapped = mapper.Map<CustomerResponse>(customer);
            var mappedList = new List<CustomerResponse> { mapped };
            return new ApiResponse<List<CustomerResponse>>(mappedList);

        }
    }
}
