using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlKhachSan.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal TotalAmount { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? PaymentMethod { get; set; }

    public string? Details { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
