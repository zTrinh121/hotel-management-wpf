using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> GetAll();
        List<BookingReservation> GetBookingReservationByCustomerId(int id);
        void Add(BookingReservation bookingReservation);
        void Update(BookingReservation bookingReservation);
        void Delete(int id);
        List<BookingReservation> GetAllReport(DateOnly startDate, DateOnly endDate);
        List<BookingReservation> GetPersonaleeReport(DateOnly startDate, DateOnly endDate);
        BookingReservation GetBookingReservationById(int id);
    }
}
