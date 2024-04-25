using System;

namespace CVBuilder.Models.Entities.Interfaces;

public interface ISoftDeletable
{
    DateTime? DeletedAt { get; set; }
}