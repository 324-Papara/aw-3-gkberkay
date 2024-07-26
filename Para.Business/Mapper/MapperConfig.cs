using AutoMapper;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Business.MapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>()
                .ForMember(dest => dest.CustomerAddresses, opt => opt.MapFrom(src => new List<CustomerAddress>()))
                .ForMember(dest => dest.CustomerPhones, opt => opt.MapFrom(src => new List<CustomerPhone>()));

            CreateMap<CustomerAddress, CustomerAddressResponse>();
            CreateMap<CustomerAddressRequest, CustomerAddress>();

            CreateMap<CustomerPhone, CustomerPhoneResponse>();
            CreateMap<CustomerPhoneRequest, CustomerPhone>();

            CreateMap<CustomerDetail, CustomerDetailResponse>();
            CreateMap<CustomerDetailRequest, CustomerDetail>();
        }
    }
}
