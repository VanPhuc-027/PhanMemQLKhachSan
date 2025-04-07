using System;
using System.Collections.Generic;

namespace QlKhachSan.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string IdNumber { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool? LoyaltyMember { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
