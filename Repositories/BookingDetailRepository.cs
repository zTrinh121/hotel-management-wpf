using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void Add(BookingDetail bookingDetail) => BookingDetailDAO.Add(bookingDetail);

        public void Delete(int bookingReservationId, int roomId) => BookingDetailDAO.Delete(bookingReservationId, roomId);

        public List<BookingDetail> GetAll() => BookingDetailDAO.GetAll();

        public BookingDetail GetBookingDetailByBookingReservationId(int id) => BookingDetailDAO.GetBookingDetailByBookingReservationId(id);

        public List<BookingDetail> GetBookingDetailByRoomId(int id) => BookingDetailDAO.GetBookingDetailByRoomId(id);

        public void Update(BookingDetail bookingDetail) => BookingDetailDAO.Update(bookingDetail);
    }
}
