using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Query
{
    public class CustomerAddressQueryHandler :
        IRequestHandler<GetAllCustomerAddressQuery, ApiResponse<List<CustomerAddressResponse>>>,
        IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<GetCustomerAddressByParameterQuery, ApiResponse<List<CustomerAddressResponse>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            List<CustomerAddress> entityList = await unitOfWork.CustomerAddressRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
            return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            CustomerAddress entity = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerAddressResponse>(entity);
            return new ApiResponse<CustomerAddressResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetCustomerAddressByParameterQuery request, CancellationToken cancellationToken)
        {
            var customers = await unitOfWork.CustomerAddressRepository.GetAllWithIncludeAsync(
                x => x.Country == request.Country,
                p => p.City,
                p => p.AddressLine,
                p => p.ZipCode
            );
            var customer = customers.FirstOrDefault();
            var mapped = mapper.Map<CustomerAddressResponse>(customer);
            var mappedList = new List<CustomerAddressResponse> { mapped };
            return new ApiResponse<List<CustomerAddressResponse>>(mappedList);

        }
    }
}
