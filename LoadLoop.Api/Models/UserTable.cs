using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class UserTable
{
    public int UsId { get; set; }

    public string UsIdNumber { get; set; } = null!;

    public string UsEmail { get; set; } = null!;

    public string UsName { get; set; } = null!;

    public string UsSurname { get; set; } = null!;

    public string UsPhone { get; set; } = null!;

    public string? UsRole { get; set; }

    public string UsPasswordHash { get; set; } = null!;

    public virtual ICollection<AdminTable> AdminTables { get; set; } = new List<AdminTable>();

    public virtual ICollection<DriverTable> DriverTables { get; set; } = new List<DriverTable>();
}
