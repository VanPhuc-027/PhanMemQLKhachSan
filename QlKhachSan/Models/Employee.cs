using System;
using System.Collections.Generic;

namespace QlKhachSan.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateOnly? HireDate { get; set; }

    public virtual User User { get; set; } = null!;
}
