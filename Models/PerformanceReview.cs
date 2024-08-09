using System;
using System.Collections.Generic;

namespace GestionEmpleados.Models;

public partial class PerformanceReview
{
    public int ReviewId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public int? ReviewerId { get; set; }

    public string? Feedback { get; set; }

    public string? Goals { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Employee? Reviewer { get; set; }
}
