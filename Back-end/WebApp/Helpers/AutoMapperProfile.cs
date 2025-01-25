using App.DTO.v1_0.Read;
using App.DTO.v1_0.Write;
using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping for ToDo (Read and Write)
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Read.ToDo>().ReverseMap(); // Read
        CreateMap<App.BLL.DTO.ToDo, App.DTO.v1_0.Write.ToDo>().ReverseMap(); // Write

        // Mapping for PaginatedResponse (Read)
        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, App.DTO.v1_0.Read.PaginatedResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)) // Map nested Items
            .ReverseMap();

        // Mapping for PaginatedResponse (Write)
        CreateMap<App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>, PaginatedResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)) // Map nested Items
            .ReverseMap();
    }
}