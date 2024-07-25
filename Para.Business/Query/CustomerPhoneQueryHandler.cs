using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Query
{
    public class CustomerPhoneQueryHandler :
        IRequestHandler<GetAllCustomerPhoneQuery, ApiResponse<List<CustomerPhoneResponse>>>,
        IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<GetCustomerPhoneByParameterQuery, ApiResponse<List<CustomerPhoneResponse>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken)
        {
            List<CustomerPhone> entityList = await unitOfWork.CustomerPhoneRepository.GetAll();
            //if (entityList is null)
            //{
            //    return new ApiResponse<List<CustomerResponse>>("Customers not found.");
            //}
            //var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(entityList);

            var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);

            return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            CustomerPhone entity = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerId);
            //if (entity is null)
            //{
            //    return new ApiResponse<CustomerResponse>("Customer not found.");
            //}
            var mapped = mapper.Map<CustomerPhoneResponse>(entity);

            return new ApiResponse<CustomerPhoneResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetCustomerPhoneByParameterQuery request, CancellationToken cancellationToken)
        {
            var customers = await unitOfWork.CustomerPhoneRepository.GetAllWithIncludeAsync(
                x => x.Phone == request.Phone,
                p => p.CountryCode
            );
            var customer = customers.FirstOrDefault();

            //if (customer is null)
            //{
            //    return new ApiResponse<List<CustomerResponse>>("Customer not found.");
            //}
            var mapped = mapper.Map<CustomerPhoneResponse>(customer);

            var mappedList = new List<CustomerPhoneResponse> { mapped };

            //return Ok(new ApiResponse<CustomerResponse?>(customer.FirstOrDefault()));
            return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);

        }
    }
}
