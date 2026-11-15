using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class TruckTable
{
    public int TruId { get; set; }

    public int DriId { get; set; }

    public string TruType { get; set; } = null!;

    public int TruMaxWeight { get; set; }

    public string TruLicensePlate { get; set; } = null!;

    public virtual DriverTable Dri { get; set; } = null!;
}
