using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId {     get; set; }

    public string? CustomerFullName { get; set; }

    public string? Telephone { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateOnly? CustomerBirthday { get; set; }

    public byte? CustomerStatus { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();

    public Customer(string? CustomerFullName, string? EmailAddress, string? Telephone,
        DateOnly? CustomerBirthday, byte? CustomerStatus, string? Password)
    {
        this.CustomerFullName = CustomerFullName;
        this.Telephone = Telephone;
        this.EmailAddress = EmailAddress;
        this.CustomerBirthday = CustomerBirthday;
        this.CustomerStatus = CustomerStatus;
        this.Password = Password;
    }

    public Customer() { }
}
