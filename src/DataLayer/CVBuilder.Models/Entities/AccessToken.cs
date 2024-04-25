using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class AccessToken : Entity<int>
{
    public string Token { get; set; }
    public DateTime? ExpiryAt { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
}