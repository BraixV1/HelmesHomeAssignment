namespace App.DTO.v1_0.Read;

public class PaginatedResponse<TEntity>
{
    public ICollection<TEntity> Items { get; set; } = default!;

    public int TotalCount { get; set; } = default;

    public int PageNumber { get; set; } = default!;

    public int PageSize { get; set; } = default!;
}