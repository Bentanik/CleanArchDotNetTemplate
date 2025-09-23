namespace _Project_.Domain.Abstractions.Interfaces;

public interface IBaseEntity
{
    DateTime? CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
}

public interface IBaseEntity<T> : IBaseEntity
{
    T Id { get; set; }
}