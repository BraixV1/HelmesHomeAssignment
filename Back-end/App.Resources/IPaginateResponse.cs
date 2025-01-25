namespace App.Resources;

public interface IPaginateResponse<T>
{
    
    IEnumerable<T> Items { get; set; }
    int PageSize { get; set; }
    int PageNumber { get; set; }
    int TotalCount { get; set; }
    
}