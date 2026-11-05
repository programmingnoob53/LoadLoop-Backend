using System;
using System.Collections.Generic;

namespace LoadLoop.api.Models;

public partial class Truck
{
    public int TruckId { get; set; }

    public string TruckImage { get; set; } = null!;

    public string? TruckLicensePlate { get; set; }

    public int UserId { get; set; }

    public string OwnerRole { get; set; } = null!;

    public int TruckCapacity { get; set; }

    public virtual UserTable User { get; set; } = null!;
}
