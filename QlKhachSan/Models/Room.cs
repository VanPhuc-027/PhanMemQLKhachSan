using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlKhachSan.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int RoomTypeId { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string Status { get; set; }

    public string? Amenities { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType RoomType { get; set; } = null!;
}
