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
    public class CustomerPhoneCommandHandler :
        IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {

            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            //var result = validator.Validate(mapped);
            //if (!result.IsValid)
            //{
            //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            //    return new ApiResponse<CustomerResponse>(string.Join(", ", errorMessages));
            //}
            await unitOfWork.CustomerPhoneRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerPhoneResponse>(mapped);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
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
    }
}
