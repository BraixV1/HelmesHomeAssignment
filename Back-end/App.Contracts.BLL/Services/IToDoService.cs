using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IToDoService: IEntityRepository<App.BLL.DTO.ToDo>
{
    
}