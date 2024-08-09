using System;
using System.Collections.Generic;

namespace GestionEmpleados.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;
}
