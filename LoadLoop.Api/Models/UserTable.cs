using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class UserTable
{
    public int UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPasswordHash { get; set; } = null!;

    public int UserAge { get; set; }

    public string UserRole { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
