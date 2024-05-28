
namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifedby { get; set; }
    public string? LastModifedBy { get; set; }
}
