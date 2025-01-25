using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Dal.DTO.ToDo, DTO.ToDo>().ReverseMap();

        CreateMap<App.Dal.DTO.PaginatedResponse<App.Dal.DTO.ToDo>, DTO.ToDo>();
    }
}