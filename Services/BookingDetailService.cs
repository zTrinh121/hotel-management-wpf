using BusinessObjects;

namespace Sevices
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailService bookingDetailService;

        public BookingDetailService()
        {
            bookingDetailService = new BookingDetailService();
        }
        public void Add(BookingDetail bookingDetail) => bookingDetailService.Add(bookingDetail);

        public void Delete(int id) => bookingDetailService.Delete(id);

        public List<BookingDetail> GetAll() => bookingDetailService.GetAll();

        public BookingDetail GetBookingDetailByBookingReservationId(int id) => bookingDetailService.GetBookingDetailByBookingReservationId(id);

        public List<BookingDetail> GetBookingDetailByRoomId(int id) => bookingDetailService.GetBookingDetailByRoomId(id);

        public void Update(BookingDetail bookingDetail) => bookingDetailService.Update(bookingDetail);
    }
}
