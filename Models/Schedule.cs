using System;
using System.Collections.Generic;

namespace GestionEmpleados.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? EmployeeId { get; set; }

    public int? ShiftId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Shift? Shift { get; set; }
}
