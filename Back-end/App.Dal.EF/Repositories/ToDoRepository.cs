using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.DAL.EF;

namespace App.Dal.EF.Repositories;

public class ToDoRepository : BaseEntityRepository<App.Domain.ToDo, App.Dal.DTO.ToDo, AppDbContext>, IToDoRepository
{
    public ToDoRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
        new DalDomainMapper<ToDo, DTO.ToDo>(mapper))
    {
        
    }
}