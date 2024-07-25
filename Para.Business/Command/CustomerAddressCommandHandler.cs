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
    public class CustomerAddressCommandHandler :
        IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {

            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            //var result = validator.Validate(mapped);
            //if (!result.IsValid)
            //{
            //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            //    return new ApiResponse<CustomerResponse>(string.Join(", ", errorMessages));
            //}
            await unitOfWork.CustomerAddressRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            //CustomerValidator validator = new CustomerValidator();
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
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
    }
}
