namespace App.DTO.v1_0.Read;

public class ReadToDoWithoutObj
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Completed { get; set; } = default!;
    
    public DateTime DueDate { get; set; } = default!;
    
    public Guid? parentTaskId { get; set; }
    
    public Guid Id { get; set; } = default!;
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public string UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}