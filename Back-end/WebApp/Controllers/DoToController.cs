

using System.Net;
using App.Contracts.BLL;
using App.DTO.v1_0.Read;
using App.Resources;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using ToDo = App.DTO.v1_0.Write.ToDo;

namespace WebApp.Controllers;


[ApiVersion( "1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/toDo")]
public class DoToController : Controller
{
    private readonly IAppBLL _bll;
    private readonly PublicDTOBllMapper<App.DTO.v1_0.Read.ToDo, App.BLL.DTO.ToDo> _ToDomapper;

    private readonly PublicDTOBllMapper<PaginatedResponse, IPaginateResponse<App.BLL.DTO.ToDo>>
        _ToDoPaginationMapper;

    public DoToController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _ToDomapper = new  PublicDTOBllMapper<App.DTO.v1_0.Read.ToDo, App.BLL.DTO.ToDo>(mapper);
        _ToDoPaginationMapper =
            new PublicDTOBllMapper<PaginatedResponse, IPaginateResponse<App.BLL.DTO.ToDo>>(mapper);
    }
    
    [HttpGet]
    [Route("ToDos")]
    [ProducesResponseType(typeof(PaginatedResponse), (int)HttpStatusCode.OK)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<PaginatedResponse>> GetAll(
        int page = 1, 
        int pagesize = 10, 
        string? name = null, 
        string? desc = null, 
        bool? done = null, 
        DateTime? dueDateTime = null)
    {
        var filters = new FilterParams
        {
            NameFilter = name,
            DescFilter = desc,
            DoneFilter = done,
            DueDateTime = dueDateTime
        };


        var result = await _bll.ToDos.GetAllFiltered(filters, page, pagesize);


        var found = _ToDoPaginationMapper.Map(result);


        return Ok(found);
    }
}