using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using App.Dal.DTO;
using App.Resources;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Service;

public class ToDoService : BaseEntityService<App.Dal.DTO.ToDo, App.BLL.DTO.ToDo, IToDoRepository>, IToDoService
{

    private readonly IMapper _mapper;
    public ToDoService(IUnitOfWork uoW, IToDoRepository repository, IMapper mapper) : base(uoW, repository,
        new BllDalMapper<ToDo, App.BLL.DTO.ToDo>(mapper))
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<IPaginateResponse<App.BLL.DTO.ToDo>> GetAllFiltered(FilterParams filterParams, int pageNumber, int pageSize)
    {
        var resultCall = await Repository.GetAllFiltered(filterParams, pageNumber, pageSize);

        var result = new App.BLL.DTO.PaginatedResponse<App.BLL.DTO.ToDo>
        {
            Items = resultCall.Items.Select(toDo => _mapper.Map<App.BLL.DTO.ToDo>(toDo)),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = resultCall.TotalCount
        };

        return result;
    }

    public async Task<App.BLL.DTO.ToDo> AddToDoAsync(App.BLL.DTO.ToDo toDo)
    {
        if (toDo.parentTaskId.HasValue && !await Repository.ExistsAsync(toDo.parentTaskId.Value))
        {
            throw new ArgumentException("Parent ToDo does not exist");
        }

        var mapped = _mapper.Map<App.Dal.DTO.ToDo>(toDo);

        if (mapped is null)
        {
            throw new ArgumentException("Mapping result is null");
        }
        
        mapped.CreatedAt = DateTime.Now;

        var added = Repository.Add(mapped);

        return _mapper.Map<App.BLL.DTO.ToDo>(added);
    }
}