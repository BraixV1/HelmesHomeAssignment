using App.Contracts.BLL.Services;
using Base.Contracts.BLL;


namespace App.Contracts.BLL;

public interface IAppBLL : IBll
{
    IToDoService ToDos { get; }
}