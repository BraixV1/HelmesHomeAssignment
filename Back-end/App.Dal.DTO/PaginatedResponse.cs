using App.Resources;

namespace App.Dal.DTO;

public class PaginatedResponse<TEntity> : IPaginateResponse<TEntity>
{
    public IEnumerable<TEntity> Items { get; set; } = default!;

    public int TotalCount { get; set; } = default;

    public int PageNumber { get; set; } = default!;

    public int PageSize { get; set; } = default!;
}