using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Data.Validator;
using Para.Schema;

namespace Para.Business.Command
{
    public class CustomerDetailCommandHandler :
        IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {

            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            //var result = validator.Validate(mapped);
            //if (!result.IsValid)
            //{
            //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            //    return new ApiResponse<CustomerResponse>(string.Join(", ", errorMessages));
            //}
            await unitOfWork.CustomerDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            mapped.Id = request.CustomerId;
            //var result = validator.Validate(mapped);
            //if (!result.IsValid)
            //{
            //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            //    return new ApiResponse(string.Join(", ", errorMessages));
            //}
            //var existingCustomer = unitOfWork.CustomerRepository.GetById(request.CustomerId);
            //if (existingCustomer == null)
            //{
            //    return new ApiResponse("Customer not found.");
            //}
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
    }
}
