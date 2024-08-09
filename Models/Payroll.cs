using System;
using System.Collections.Generic;

namespace GestionEmpleados.Models;

public partial class Payroll
{
    public int PayrollId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Salary { get; set; }

    public decimal? Bonuses { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? NetSalary { get; set; }

    public DateOnly? PayrollDate { get; set; }

    public virtual Employee? Employee { get; set; }
}
