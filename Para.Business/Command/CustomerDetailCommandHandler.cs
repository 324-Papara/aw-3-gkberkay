using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Command
{
    public class CustomerDetailCommandHandler :
        IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>,
        IRequestHandler<ValidateCustomerDetailCommand, ApiResponse>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<CustomerDetailRequest> validator;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerDetailRequest> validator)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            await unitOfWork.CustomerDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            mapped.Id = request.CustomerId;
            unitOfWork.CustomerDetailRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerDetailRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CustomerDetailRequest, cancellationToken);

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
