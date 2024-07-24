﻿using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Data.Validator;
using Para.Schema;

namespace Para.Business.Command
{
    public class CustomerCommandHandler :
        IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
        IRequestHandler<UpdateCustomerCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.CustomerNumber = new Random().Next(1000000,9999999);
            var result = validator.Validate(mapped);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return new ApiResponse<CustomerResponse>(string.Join(", ", errorMessages));
            }
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerResponse>(mapped);
            return new ApiResponse<CustomerResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.Id = request.CustomerId;
            var result = validator.Validate(mapped);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return new ApiResponse(string.Join(", ", errorMessages));
            }
            var existingCustomer = unitOfWork.CustomerRepository.GetById(request.CustomerId);
            if (existingCustomer == null)
            {
                return new ApiResponse("Customer not found.");
            }
            unitOfWork.CustomerRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
