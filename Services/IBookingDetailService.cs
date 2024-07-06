using BusinessObjects;

namespace Sevices
{
    public interface IBookingDetailService
    {
        List<BookingDetail> GetAll();
        BookingDetail GetBookingDetailByBookingReservationId(int id);
        List<BookingDetail> GetBookingDetailByRoomId(int id);
        void Add(BookingDetail bookingDetail);
        void Update(BookingDetail bookingDetail);
        void Delete(int bookingReservationId, int roomId);
    }
}
