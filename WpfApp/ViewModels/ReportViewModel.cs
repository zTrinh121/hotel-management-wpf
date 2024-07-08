using BusinessObjects;
using Repositories;
using Sevices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private BookingReservation currentBookingReservation;
        private BookingDetail currentBookingDetail;
        private readonly IBookingReservationService bookingReservationService;
        private readonly IBookingDetailService bookingDetailService;
        private readonly ICustomerService customerService;
        private ObservableCollection<BookingReservation> bookingReservations;
        private ObservableCollection<BookingDetail> bookingDetails;
        private readonly IRoomInfomationService roomInfomationService;
        public int CustomerId;
        public ObservableCollection<string> Statuses { get; set; }


        public ReportViewModel(IBookingReservationService bookingReservationService,
                                IBookingDetailService bookingDetailService,
                                ICustomerService customerService,
                                IRoomInfomationService roomInfomationService,
                                int CustomerId)
        {
            this.bookingDetailService = bookingDetailService;
            this.bookingReservationService = bookingReservationService;
            this.customerService = customerService;
            this.roomInfomationService = roomInfomationService;
            currentBookingDetail = new BookingDetail();
            this.CustomerId = CustomerId;
            LoadBookingReservation(CustomerId);
            LoadBookingDetails(CustomerId);
            currentBookingReservation = new BookingReservation();
            Statuses = new ObservableCollection<string> { "Success", "Fail" };

        }

        public ObservableCollection<BookingReservation> BookingReservations
        {
            get
            {
                if (bookingReservations == null)
                {
                    bookingReservations = new ObservableCollection<BookingReservation>(bookingReservationService.GetAll());
                }
                return bookingReservations;
            }
            set
            {
                bookingReservations = value;
                OnPropertyChanged();
            }
        }



        public List<BookingReservation> LoadBookingReservation(int CustomerId)
        {
            return bookingReservationService.GetBookingReservationByCustomerId(CustomerId);
        }

        public BookingReservation CurrentBookingReservation
        {
            get { return currentBookingReservation; }
            set
            {
                currentBookingReservation = value;
                OnPropertyChanged(nameof(CurrentBookingReservation));
            }
        }

        //BookingDetail
        public BookingDetail CurrentBookingDetail
        {
            get { return currentBookingDetail; }
            set
            {
                currentBookingDetail = value;
                OnPropertyChanged(nameof(CurrentBookingDetail));
            }
        }

        public ObservableCollection<BookingDetail> BookingDetails
        {
            get
            {
                return bookingDetails;
            }
            set
            {
                bookingDetails = value;
                OnPropertyChanged();
            }
        }

        public void LoadBookingDetails(int CustomerId)
        {
            var allBookingDetails = bookingDetailService.GetAll();
            
            foreach (var booking in allBookingDetails)
            {
                booking.BookingReservation = bookingReservationService.GetBookingReservationById(booking.BookingReservationId);
                if(booking.BookingReservation.CustomerId == CustomerId)
                {
                    booking.BookingReservation.Customer = customerService.GetCustomerById(booking.BookingReservation.CustomerId);
                    booking.Room = roomInfomationService.GetRoomInfomationById(booking.RoomId);
                }

            }
            BookingDetails = new ObservableCollection<BookingDetail>(allBookingDetails.Where(bd => bd.BookingReservation.CustomerId == CustomerId).ToList());


        }




        //NotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
