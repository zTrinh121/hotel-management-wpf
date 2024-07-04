using BusinessObjects;
using DataAccessLayer;
using Repositories;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository bookingReservationRepository;

        public BookingReservationService()
        {
            bookingReservationRepository = new BookingReservationRepository();
        }
        public void Add(BookingReservation bookingReservation) => bookingReservationRepository.Add(bookingReservation);

        public void Delete(int id) => bookingReservationRepository.Delete(id);

        public List<BookingReservation> GetAll() => bookingReservationRepository.GetAll();

        public BookingReservation GetBookingReservationByCustomerId(int id) => bookingReservationRepository.GetBookingReservationByCustomerId(id);

        public void Update(BookingReservation bookingReservation) => bookingReservationRepository.Update(bookingReservation);
    }
}
