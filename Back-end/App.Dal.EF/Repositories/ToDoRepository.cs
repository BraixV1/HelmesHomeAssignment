using System.Collections;
using App.Contracts.DAL.Repositories;
using App.Dal.DTO;
using App.Resources;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using ToDo = App.Domain.ToDo;

namespace App.Dal.EF.Repositories;

public class ToDoRepository : BaseEntityRepository<App.Domain.ToDo, App.Dal.DTO.ToDo, AppDbContext>, IToDoRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public ToDoRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
        new DalDomainMapper<ToDo, DTO.ToDo>(mapper))
    {
        _mapper = mapper;
        _context = dbContext;

    }

    public async Task<IPaginateResponse<DTO.ToDo>> GetAllFiltered(FilterParams filterParams, int pageNumber, int pageSize)
    {
        var query = _context.Todos.AsQueryable();
        
        if (!string.IsNullOrEmpty(filterParams.NameFilter))
        {
            query = query.Where(t => t.Title.ToLower().Contains(filterParams.NameFilter.ToLower()));
        }

        if (!string.IsNullOrEmpty(filterParams.DescFilter))
        {
            query = query.Where(t => t.Description.ToLower().Contains(filterParams.DescFilter.ToLower()));
        }

        if (filterParams.DoneFilter.HasValue)
        {
            query = query.Where(t => t.Completed == filterParams.DoneFilter.Value);
        }

        if (filterParams.DueDateTime.HasValue)
        {
            query = query.Where(t => t.DueDate.ToUniversalTime() <= filterParams.DueDateTime.Value);
        }
        
        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var mappedItems = items.Select(toDo => Mapper.Map(toDo));
        
        return new PaginatedResponse<DTO.ToDo>
        {
            Items = mappedItems ?? Enumerable.Empty<DTO.ToDo>(),
            PageSize = pageSize,
            PageNumber = pageNumber,
            TotalCount = totalCount
        };
    }
}