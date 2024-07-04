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
            this.bookingDetailService = bookingDetailService;
            currentBookingReservation = new BookingReservation();
            Statuses = new ObservableCollection<string> { "Success", "Fail" };
            LoadBookingDetails();
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
            var allBookingDetails = bookingDetailService.GetAll();
            BookingDetails = new ObservableCollection<BookingDetail>(allBookingDetails);

            foreach (var bookingReservation in BookingReservations)
            {
                bookingReservation.BookingDetails = new List<BookingDetail>(
                    allBookingDetails.Where(bd => bd.BookingReservationId == bookingReservation.BookingReservationId)
                );
            }
        }

        public void SaveBookingReservation()
        {
            var newBookingReservation = new BookingReservation
            {
                BookingDate = CurrentBookingReservation.BookingDate,
                TotalPrice = CurrentBookingReservation.TotalPrice,
                Customer = CurrentBookingReservation.Customer,
                BookingStatus = CurrentBookingReservation.BookingStatus
                
            };
            bookingReservationService.Add(newBookingReservation);
            LoadBookingReservation();
            ResetInput();
        }

        public void UpdateBookingReservation()
        {
            bookingReservationService.Update(currentBookingReservation);
            LoadBookingReservation();
            ResetInput();
        }

        public void DeleteBookingReservation()
        {
            bookingReservationService.Delete(currentBookingReservation.BookingReservationId);
            LoadBookingReservation();
            ResetInput();
        }


        public void ResetInput()
        {
            currentBookingReservation = new BookingReservation();
            OnPropertyChanged(nameof(CurrentBookingReservation));
        }
        //Notify 
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
