using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork: IUnitOfWork
{
    IToDoRepository ToDos { get; }
}