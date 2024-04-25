using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class File : Entity<int>
{
    public string Path { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}