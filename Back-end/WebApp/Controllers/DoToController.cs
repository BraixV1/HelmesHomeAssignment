

using System.Net;
using App.BLL.DTO;
using App.Contracts.BLL;
using App.DTO.v1_0;
using App.DTO.v1_0.Read;
using App.DTO.v1_0.Write;
using App.Resources;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers;


[ApiVersion( "1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/toDo")]
public class DoToController : Controller
{
    private readonly IAppBLL _bll;
    private readonly PublicDTOBllMapper<App.DTO.v1_0.Read.ReadToDo, App.BLL.DTO.ToDo> _ToDomapper;
    private readonly PublicDTOBllMapper<App.DTO.v1_0.Write.WriteToDo, App.BLL.DTO.ToDo> _WriteToDoMapper;
    private readonly PublicDTOBllMapper<PaginatedResponse, IPaginateResponse<App.BLL.DTO.ToDo>>
        _ToDoPaginationMapper;
    

    public DoToController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _ToDomapper = new  PublicDTOBllMapper<App.DTO.v1_0.Read.ReadToDo, App.BLL.DTO.ToDo>(mapper);
        _WriteToDoMapper = new PublicDTOBllMapper<WriteToDo, ToDo>(mapper);
        _ToDoPaginationMapper =
            new PublicDTOBllMapper<PaginatedResponse, IPaginateResponse<App.BLL.DTO.ToDo>>(mapper);
    }
    
    
    /// <summary>
    /// Returns page of toDos based of the filter parameters
    /// filters work as AND parameter
    /// </summary>
    /// <param name="page">page of toDo's base value is 1</param>
    /// <param name="pagesize">count of toDos in single page Base value is 10</param>
    /// <param name="name">optional filter parameter that filters based on name</param>
    /// <param name="desc">optional filter parameter that filters based on content of toDo</param>
    /// <param name="done">optional filter parameter that filters based on the done or not</param>
    /// <param name="dueDateTime">optional filter that returns toDos that have to be done specific date</param>
    /// <returns></returns>
    [HttpGet]
    [Route("ToDo")]
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
    
    /// <summary>
    /// Get specific toDo from the databse based on id
    /// </summary>
    /// <param name="id">id of the ToDo</param>
    /// <returns>found toDo if it exists</returns>
    [HttpGet]
    [Route("ToDo/{id:guid}")]
    [ProducesResponseType(typeof(PaginatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.NotFound)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<PaginatedResponse>> Get(Guid id)
    {
        
        var result = await _bll.ToDos.FirstOrDefaultAsync(id);

        if (result is null)
        {
            return NotFound(new RestAPIErrorResponse { Error = "ToDo not found", Status = HttpStatusCode.NotFound });
        }

        var found = _ToDomapper.Map(result);


        return Ok(found);
    }

    /// <summary>
    /// Creeates new doTo and saves it to the database
    /// </summary>
    /// <param name="readToDo"> toDo that will be saved to the database </param>
    /// <returns>saved toDo if save was successful</returns>
    [HttpPost]
    [Route("ToDo")]
    [ProducesResponseType(typeof(App.DTO.v1_0.Read.ReadToDo), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.BadRequest)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<App.DTO.v1_0.Read.ReadToDo>> Post(WriteToDo toDo)
    {
        try
        {
            var mapped = _WriteToDoMapper.Map(toDo);

            if (mapped is null)
            {
                return BadRequest(new RestAPIErrorResponse
                    { Error = "Mapping result was null", Status = HttpStatusCode.BadRequest });
            }
            
            var result = await _bll.ToDos.AddToDoAsync(mapped);


            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new RestAPIErrorResponse { Error = e.Message, Status = HttpStatusCode.BadRequest });
        }

    }

    /// <summary>
    /// updates doTo in the database
    /// </summary>
    /// <param name="id">id of the toDo that needs to be updated</param>
    /// <param name="toDo">new toDo that will overwrite the previous one</param>
    /// <returns>updated Todo was update was successful</returns>
    [HttpPut]
    [Route("ToDo/{id:guid}")]
    [ProducesResponseType(typeof(WriteToDo), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.NotFound)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<WriteToDo>> Put(Guid id, WriteToDo toDo)
    {
        try
        {
            var mapped = _WriteToDoMapper.Map(toDo);

            if (mapped is null)
            {
                return BadRequest(new RestAPIErrorResponse
                    { Error = "Mapping result was null", Status = HttpStatusCode.BadRequest });
            }
            
            var result = _bll.ToDos.Update(mapped);

            await _bll.SaveChangesAsync();

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new RestAPIErrorResponse
            {
                Error = e.Message,
                Status = HttpStatusCode.BadRequest
            });
        }
    }

    /// <summary>
    ///  Deletes toDo from the database based on id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if deletion was successful</returns>
    [HttpDelete]
    [Route("Todo/{id:guid}")]
    [ProducesResponseType(typeof(App.DTO.v1_0.Read.ReadToDo), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(RestAPIErrorResponse), (int)HttpStatusCode.NotFound)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        try
        {
            await _bll.ToDos.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(new RestAPIErrorResponse
            {
                Error = e.Message,
                Status = HttpStatusCode.BadRequest
            });
        }

    }
    
}