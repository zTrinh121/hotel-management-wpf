using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void Add(BookingReservation bookingReservation) => BookingReservationDAO.Add(bookingReservation);

        public void Delete(int id) => BookingReservationDAO.Delete(id);

        public List<BookingReservation> GetAll() => BookingReservationDAO.GetAll();

        public List<BookingReservation> GetAllReport(DateOnly startDate, DateOnly endDate) => BookingReservationDAO.GetAllReport(startDate, endDate);

        public List<BookingReservation> GetBookingReservationByCustomerId(int id) => BookingReservationDAO.GetBookingReservationByCustomerId(id);

        public List<BookingReservation> GetPersonaleeReport(DateOnly startDate, DateOnly endDate) => BookingReservationDAO.GetPersonaleeReport(startDate, endDate);

        public void Update(BookingReservation bookingReservation) => BookingReservationDAO.Update(bookingReservation);
        public BookingReservation GetBookingReservationById(int id) => BookingReservationDAO.GetBookingReservationById(id);
    }
}
