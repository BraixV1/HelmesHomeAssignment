namespace App.DTO.v1_0.Write;

public class WriteToDo
{
    
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Completed { get; set; } = default!;
    
    public Guid? parentTaskId { get; set; }
    
    public WriteToDo? parentTask { get; set; }
    
    public DateTime DueDate { get; set; } = default!;
    
    public string UpdatedBy { get; set; } = default!;
}