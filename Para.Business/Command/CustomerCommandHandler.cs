﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Command
{
    public class CustomerCommandHandler :
        IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
        IRequestHandler<UpdateCustomerCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerCommand, ApiResponse>,
        IRequestHandler<ValidateCustomerCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<CustomerRequest> validator;

        public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerRequest> validator)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.CustomerNumber = new Random().Next(1000000,9999999);
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerResponse>(mapped);
            return new ApiResponse<CustomerResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.Id = request.CustomerId;
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

        public async Task<ApiResponse> Handle(ValidateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CustomerRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse()
                {
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                };
                return errorResponse;
            }

            return new ApiResponse();
        }
    }
}
