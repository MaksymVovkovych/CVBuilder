using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class RefreshToken : Entity<int>
{
    public string JwtId { get; set; }
    public string Token { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiryAt { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
    
}