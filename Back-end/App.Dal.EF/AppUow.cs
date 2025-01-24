using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Dal.EF.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.Dal.EF;

public class AppUow : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;

    public AppUow(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IToDoRepository? _toDoRepository;

    public IToDoRepository ToDos => _toDoRepository ?? new ToDoRepository(UowDbContext, _mapper);
}