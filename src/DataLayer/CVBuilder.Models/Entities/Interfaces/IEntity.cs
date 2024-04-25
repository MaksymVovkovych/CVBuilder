using System;

namespace CVBuilder.Models.Entities.Interfaces;

public class Entity<T> : IEntity<T> where T : struct

{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}

public interface IEntity<T> : ISoftDeletable where T : struct
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}