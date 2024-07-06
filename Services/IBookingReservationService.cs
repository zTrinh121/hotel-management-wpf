using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public interface IBookingReservationService
    {
        List<BookingReservation> GetAll();
        List<BookingReservation> GetBookingReservationByCustomerId(int id);
        void Add(BookingReservation bookingReservation);
        void Update(BookingReservation bookingReservation);
        void Delete(int id);
        BookingReservation GetBookingReservationById(int id);
    }
}
