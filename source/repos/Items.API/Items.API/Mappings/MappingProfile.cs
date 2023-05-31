using AutoMapper;
using Items.DB.Entities;

namespace User.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbItem, DtoItem>()
                .ReverseMap();
            CreateMap<DbOrder, DtoOrder>() 
                .ReverseMap();
            CreateMap<DbShipment, DtoShipment>()
                .ReverseMap();
            CreateMap<DbOrderItem, DtoOrderItem>()
                .ReverseMap();
            CreateMap<DbShipmentItem, DtoShipmentItem>()
                .ReverseMap();
        }
    }
}
