using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class DriverTable
{
    public int DriId { get; set; }

    public decimal DriSalary { get; set; }

    public bool IsAvailable { get; set; }

    public int UsId { get; set; }

    public virtual ICollection<TruckTable> TruckTables { get; set; } = new List<TruckTable>();

    public virtual UserTable Us { get; set; } = null!;
}
