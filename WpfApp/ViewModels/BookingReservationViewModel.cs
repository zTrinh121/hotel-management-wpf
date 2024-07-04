using BusinessObjects;
using Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    public class BookingReservationViewModel : INotifyPropertyChanged
    {
        private BookingReservation currentBookingReservation;
        private readonly IBookingReservationService bookingReservationService;
        private readonly IBookingDetailService bookingDetailService;
        private ObservableCollection<BookingReservation> bookingReservations;
        private ObservableCollection<BookingDetail> bookingDetails;
        public ObservableCollection<string> Statuses { get; set; }

        public BookingReservationViewModel(IBookingReservationService bookingReservationService, IBookingDetailService bookingDetailService)
        {
            this.bookingReservationService = bookingReservationService;
            //this.bookingDetailService = bookingDetailService;
            LoadBookingReservation();
            //LoadBookingDetails();
            currentBookingReservation = new BookingReservation();
            Statuses = new ObservableCollection<string> { "Success", "Fail" };
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

        public ObservableCollection<BookingReservation> BookingReservations
        {
            get
            {
                ObservableCollection<BookingReservation> bookingReservations = BookingReservations;
                foreach (var bookingReservation in bookingReservations)
                {
                    foreach (var bookingDetail in bookingDetails)
                    {
                        if (bookingDetail.BookingReservationId == bookingReservation.BookingReservationId)
                        {
                            bookingReservation.BookingDetails = (ICollection<BookingDetail>)bookingDetail;
                            break;
                        }
                    }
                }
                return bookingReservations;
            }
            set
            {
                bookingReservations = value;
                OnPropertyChanged();
            }
        }

        public void LoadBookingReservation()
        {
            BookingReservations = new ObservableCollection<BookingReservation>(bookingReservationService.GetAll());
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

        public void LoadBookingDetails()
        {
            BookingDetails = new ObservableCollection<BookingDetail>(bookingDetailService.GetAll());
        }

        //Notify 
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
