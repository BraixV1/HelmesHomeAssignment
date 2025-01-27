using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IToDoService: IEntityRepository<App.BLL.DTO.ToDo>, IToDoRepositoryCustom<App.BLL.DTO.ToDo>, IToDoServiceCustom<App.BLL.DTO.ToDo>
{
    
}

public interface IToDoServiceCustom<TEntity>
{
    Task<TEntity> AddToDoAsync(TEntity entity);

    Task<TEntity> UpdateToDoAsync(TEntity entity);
}