using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Holiday : Entity<int>
{
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}