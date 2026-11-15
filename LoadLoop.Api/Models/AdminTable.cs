using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class AdminTable
{
    public int AdId { get; set; }

    public int UsId { get; set; }

    public decimal AdSalary { get; set; }

    public virtual UserTable Us { get; set; } = null!;
}
