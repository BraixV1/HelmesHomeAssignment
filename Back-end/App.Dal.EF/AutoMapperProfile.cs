using AutoMapper;

namespace App.Dal.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.ToDo, App.Dal.DTO.ToDo>().ReverseMap();
    }
}