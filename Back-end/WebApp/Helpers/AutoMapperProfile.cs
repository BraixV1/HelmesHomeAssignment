using App.DTO.v1_0.Read;
using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Read.ToDo>().ReverseMap();
        
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Read.ToDo>().ReverseMap();

        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, App.DTO.v1_0.Read.PaginatedResponse<ToDo>>()
            .ReverseMap();
        
        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, App.DTO.v1_0.Read.PaginatedResponse<ToDo>>()
            .ReverseMap();
        
        
        
        
    }
    
}