using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using System.ComponentModel.DataAnnotations;

namespace Para.Business.Command
{
    public class CustomerAddressCommandHandler :
        IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>,
        IRequestHandler<ValidateCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<CustomerAddressRequest> validator;

        public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerAddressRequest> validator)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            await unitOfWork.CustomerAddressRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            mapped.Id = request.CustomerId;
            unitOfWork.CustomerAddressRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerAddressRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CustomerAddressRequest, cancellationToken);

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
