using Base.Contracts.Domain;

namespace App.Dal.DTO;

public abstract class BaseEntity : IDomainEntityId , IDomainEntityMetadata
{
    public Guid Id { get; set; } = default!;
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public string UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = default!;
}