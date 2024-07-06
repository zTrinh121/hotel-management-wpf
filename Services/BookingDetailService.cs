using BusinessObjects;
using Repositories;

namespace Sevices
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository bookingDetailRepository;

        public BookingDetailService()
        {
            bookingDetailRepository = new BookingDetailRepository();
        }
        public void Add(BookingDetail bookingDetail) => bookingDetailRepository.Add(bookingDetail);

        public void Delete(int bookingReservationId, int roomId) => bookingDetailRepository.Delete(bookingReservationId, roomId);

        public List<BookingDetail> GetAll() => bookingDetailRepository.GetAll();

        public BookingDetail GetBookingDetailByBookingReservationId(int id) => bookingDetailRepository.GetBookingDetailByBookingReservationId(id);

        public List<BookingDetail> GetBookingDetailByRoomId(int id) => bookingDetailRepository.GetBookingDetailByRoomId(id);

        public void Update(BookingDetail bookingDetail) => bookingDetailRepository.Update(bookingDetail);
    }
}
