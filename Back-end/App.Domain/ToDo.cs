
namespace App.Domain;

public class ToDo : BaseEntity
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTime DueDate { get; set; } = default!;
    
    public bool Completed { get; set; } = default!;
    
    public Guid? parentTaskId { get; set; }
    
    public ToDo? parentTask { get; set; }
    
    public ICollection<ToDo>? subTasks { get; set; }
    
}