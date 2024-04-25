using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Language : Entity<int>, IOrderlyEntity
{
    public string Name { get; set; }
    public int Order { get; set; }
}