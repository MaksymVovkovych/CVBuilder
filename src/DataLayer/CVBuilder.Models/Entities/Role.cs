using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Models.Entities;

public class Role : IdentityRole<int>, IEntity<int>
{
    public ICollection<User> Users { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}