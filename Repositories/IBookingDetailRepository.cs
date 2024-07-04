using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetAll();
        BookingDetail GetBookingDetailByBookingReservationId(int id);
        List<BookingDetail> GetBookingDetailByRoomId(int id);
        void Add(BookingDetail bookingDetail);
        void Update(BookingDetail bookingDetail);
        void Delete(int id);
    }
}
