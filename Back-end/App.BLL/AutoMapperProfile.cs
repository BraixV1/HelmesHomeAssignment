using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Dal.DTO.ToDo, DTO.ToDo>().ReverseMap();
    }
}