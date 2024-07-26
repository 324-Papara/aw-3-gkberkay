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
    public class CustomerPhoneCommandHandler :
        IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<ValidateCustomerPhoneCommand, ApiResponse>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<CustomerPhoneRequest> validator;

        public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerPhoneRequest> validator)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            await unitOfWork.CustomerPhoneRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerPhoneResponse>(mapped);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            mapped.Id = request.CustomerId;
            unitOfWork.CustomerPhoneRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerPhoneRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ValidateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CustomerPhoneRequest, cancellationToken);

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
