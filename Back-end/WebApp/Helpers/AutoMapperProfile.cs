using App.DTO.v1_0.Read;
using App.DTO.v1_0.Write;
using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Read.ReadToDo>().ReverseMap();
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Write.WriteToDo>().ReverseMap(); 
        
        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, App.DTO.v1_0.Read.PaginatedResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
        
        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, PaginatedResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
    }
}