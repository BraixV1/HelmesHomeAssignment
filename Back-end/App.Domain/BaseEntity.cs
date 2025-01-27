using Base.Contracts.Domain;

namespace App.Domain;

public abstract class BaseEntity : IDomainEntityId
{
    public Guid Id { get; set; } = default!;
    public string? CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public string? UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = default!;
}