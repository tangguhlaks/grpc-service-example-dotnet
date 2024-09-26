using AutoMapper;
using GrpcServiceExample.Model;
using GrpcServiceExample.Protos;

namespace GrpcServiceExample.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();
        }
    }
}
