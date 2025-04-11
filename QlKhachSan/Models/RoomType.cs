using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlKhachSan.Models;

public partial class RoomType
{
    
    public int RoomTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal PriceDay { get; set; }

    public decimal? PriceHour { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
