using App.Dal.DTO;
using App.Resources;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IToDoRepository : IEntityRepository<App.Dal.DTO.ToDo>, IToDoRepositoryCustom<App.Dal.DTO.ToDo>
{
    
}

public interface IToDoRepositoryCustom<TEntity>
{
    Task<IPaginateResponse<TEntity>> GetAllFiltered(FilterParams filterParams, int pageNumber, int pageSize);
}