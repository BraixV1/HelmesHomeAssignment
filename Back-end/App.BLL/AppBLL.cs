using App.BLL.Service;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Dal.EF;
using App.Dal.EF.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBll<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;

    public readonly IAppUnitOfWork _uow;

    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }
    
    private IToDoService? _toDoService { get; }

    public IToDoService ToDos => _toDoService ?? new ToDoService(_uow, _uow.ToDos, _mapper);

}