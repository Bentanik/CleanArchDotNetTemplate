namespace _Project_.Domain.Abstractions.Interfaces;

public interface IBaseEntity
{
    DateTime? CreatedDate { get; set; }
    DateTime? ModifiedDate { get; set; }
}

public interface IBaseEntity<T> : IBaseEntity
{
    T Id { get; set; }
}