using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using App.Dal.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Service;

public class ToDoService : BaseEntityService<App.Dal.DTO.ToDo, App.BLL.DTO.ToDo, IToDoRepository>, IToDoService
{
    public ToDoService(IUnitOfWork uoW, IToDoRepository repository, IMapper mapper) : base(uoW, repository, new BllDalMapper<ToDo, App.BLL.DTO.ToDo>(mapper)) {}
}