using BusinessObjects;
using Repositories;
using Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private readonly IBookingReservationService bookingReservationService;
        private readonly IBookingDetailService bookingDetailService;
        private ObservableCollection<BookingReservation> bookingReservations;
        private ObservableCollection<BookingDetail> bookingDetails;
        public ObservableCollection<string> Statuses { get; set; }


        public ReportViewModel(IBookingReservationService bookingReservationService, IBookingDetailService bookingDetailService)
        {
            this.bookingDetailService = bookingDetailService;
            this.bookingReservationService = bookingReservationService;
            //LoadRoomReservation();
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

        //NotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
