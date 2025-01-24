using AutoMapper;

namespace App.Dal.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Domain.ToDo, DTO.ToDo>().ReverseMap();
    }
}