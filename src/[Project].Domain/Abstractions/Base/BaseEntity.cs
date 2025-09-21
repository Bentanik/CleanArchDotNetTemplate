namespace _Project_.Domain.Abstractions.Base;

public abstract class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; } = default!;
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}