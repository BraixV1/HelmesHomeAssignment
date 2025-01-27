using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0.Write;

public class WriteToDoWithoutObj
{
    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public bool Completed { get; set; } = default!;
    
    public Guid? parentTaskId { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; } = default!;
    
    public string? UpdatedBy { get; set; } = default!;

    public string? CreatedBy { get; set; } = default!;
}