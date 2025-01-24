using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IToDoRepository : IEntityRepository<App.Dal.DTO.ToDo>
{
    
}