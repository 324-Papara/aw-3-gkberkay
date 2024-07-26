using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Business.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Business.Query
{
    public class CustomerDetailQueryHandler :
        IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>,
        IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<GetCustomerDetailByParameterQuery, ApiResponse<List<CustomerDetailResponse>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDetail> entityList = await unitOfWork.CustomerDetailRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerDetailResponse>>(entityList);
            return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            CustomerDetail entity = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerDetailResponse>(entity);
            return new ApiResponse<CustomerDetailResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetCustomerDetailByParameterQuery request, CancellationToken cancellationToken)
        {
            var customers = await unitOfWork.CustomerDetailRepository.GetAllWithIncludeAsync(
                x => x.FatherName == request.FatherName,
                p => p.MotherName,
                p => p.EducationStatus,
                p => p.MountlyIncome,
                p =>p.Occupation
            );
            var customer = customers.FirstOrDefault();
            var mapped = mapper.Map<CustomerDetailResponse>(customer);
            var mappedList = new List<CustomerDetailResponse> { mapped };
            return new ApiResponse<List<CustomerDetailResponse>>(mappedList);

        }
    }
}
