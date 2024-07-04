using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class RoomInformation
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? RoomDetailDescription { get; set; }

    public int? RoomMaxCapacity { get; set; }

    public int RoomTypeId { get; set; }

    public byte? RoomStatus { get; set; }

    public decimal? RoomPricePerDay { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;

    public RoomInformation(string RoomNumber, string? RoomDetailDescription, int? RoomMaxCapacity, byte? RoomStatus, decimal? RoomPricePerDay, int RoomTypeId)
    {
        this.RoomNumber = RoomNumber;
        this.RoomDetailDescription = RoomDetailDescription;
        this.RoomMaxCapacity = RoomMaxCapacity;
        this.RoomStatus = RoomStatus;
        this.RoomPricePerDay = RoomPricePerDay;
        this.RoomTypeId = RoomTypeId;
    }

    public RoomInformation()
    {

    }
}
