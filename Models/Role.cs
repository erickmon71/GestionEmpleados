using System;
using System.Collections.Generic;

namespace GestionEmpleados.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
