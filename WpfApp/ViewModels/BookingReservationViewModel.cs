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
        private BookingDetail currentBookingDetail;
        private readonly IBookingReservationService bookingReservationService;
        private readonly IBookingDetailService bookingDetailService;
        private readonly ICustomerService customerService;
        private readonly IRoomInfomationService roomInfomationService;
        private string searchQuery;
        private ObservableCollection<BookingDetail> filteredBookingDetails;
        private ObservableCollection<BookingReservation> bookingReservations;
        private ObservableCollection<BookingDetail> bookingDetails;
        private ObservableCollection<RoomInformation> roomInformations;
        private ObservableCollection<Customer> customers;         
        public ObservableCollection<string> Statuses { get; set; }

        public BookingReservationViewModel(IBookingReservationService bookingReservationService, 
                                        IBookingDetailService bookingDetailService,
                                        ICustomerService customerService,
                                        IRoomInfomationService roomInfomationService)
        {
            this.bookingReservationService = bookingReservationService;
            this.bookingDetailService = bookingDetailService;
            this.customerService = customerService;
            this.roomInfomationService = roomInfomationService;
            currentBookingReservation = new BookingReservation();
            currentBookingDetail = new BookingDetail();
            Statuses = new ObservableCollection<string> { "Success", "Fail" };
            LoadBookingDetails();
            LoadBookingReservation();
            LoadCustomers();
            LoadRoomInformations();
        }

        public BookingReservation CurrentBookingReservation
        {
            get 
            {   
                //ObservableCollection<RoomInformation> roomInformations = 
                return currentBookingReservation;
            }
            set
            {
                currentBookingReservation = value;
                OnPropertyChanged(nameof(CurrentBookingReservation));
                OnPropertyChanged(nameof(CurrentBookingDetail.Room.RoomId));
            }
        }

        public BookingDetail CurrentBookingDetail
        {
            get { return currentBookingDetail; }
            set
            {
                currentBookingDetail = value;
                OnPropertyChanged(nameof(CurrentBookingDetail));
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
            foreach (var booking in BookingReservations)
            {
                booking.Customer = customerService.GetCustomerById(booking.CustomerId);
            }
        }

        public ObservableCollection<BookingDetail> BookingDetails
        {
            get
            {
                //ObservableCollection<RoomInformation> roomInformations = RoomInformations;
                //ObservableCollection<Customer> customers = Customers;
                //foreach(var booking in BookingDetails)
                //{
                //    foreach(var customer in customers)
                //    {
                //        if(booking.BookingReservation.CustomerId == customer.CustomerId)
                //        {
                //            booking.BookingReservation.Customer = customer;
                //        }
                //    }
                //    foreach(var room in roomInformations)
                //    {
                //        if(booking.RoomId == room.RoomId)
                //        {
                //            booking.RoomId = room.RoomId;
                //        }
                //    }
                //}
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
            LoadCustomers(); 
            LoadRoomInformations();
            var allBookingDetails = bookingDetailService.GetAll();
            BookingDetails = new ObservableCollection<BookingDetail>(allBookingDetails);
            foreach (var booking in BookingDetails)
            {
                booking.Room = roomInfomationService.GetRoomInfomationById(booking.RoomId);
                booking.BookingReservation = bookingReservationService.GetBookingReservationById(booking.BookingReservationId);
                booking.BookingReservation.Customer = customerService.GetCustomerById(booking.BookingReservation.CustomerId);
            }

        }

        public ObservableCollection<RoomInformation> RoomInformations
        {
            get
            {
                return roomInformations;
            }
            set
            {
                roomInformations = value;
                OnPropertyChanged();
            }
        }

        public void LoadRoomInformations()
        {
            RoomInformations = new ObservableCollection<RoomInformation>(roomInfomationService.GetAll());
        }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(customerService.GetAll());
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BookingDetail> FilteredBookingDetails
        {
            get => filteredBookingDetails;
            set
            {
                filteredBookingDetails = value;
                OnPropertyChanged();
            }
        }

        public void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredBookingDetails = new ObservableCollection<BookingDetail>(BookingDetails);
                return;
            }

            var query = BookingDetails.Where(bd =>
                bd.BookingReservationId.ToString().Contains(SearchQuery) ||
                bd.BookingReservation.BookingDate.ToString().Contains(SearchQuery) ||
                bd.BookingReservation.TotalPrice.ToString().Contains(SearchQuery) ||
                bd.BookingReservation.Customer.CustomerFullName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                bd.BookingReservation.BookingStatus.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                bd.Room.RoomNumber.Contains(SearchQuery) ||
                bd.StartDate.ToString().Contains(SearchQuery) ||
                bd.EndDate.ToString().Contains(SearchQuery)
            );

            FilteredBookingDetails = new ObservableCollection<BookingDetail>(query.ToList());
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

        public void SaveBookingDetail( BookingDetail bookingDetail, BookingReservation bookingReservation)
        {
           

            bookingDetailService.Add(bookingDetail);
            bookingReservationService.Add(bookingReservation);

            LoadBookingDetails();
            LoadBookingReservation();
            ResetInput();
        }

        public void UpdateBookingReservation()
        {
            bookingReservationService.Update(currentBookingReservation);
            LoadBookingReservation();
            ResetInput();
        }

        public void UpdateBookingDetail()
        {
            
            bookingDetailService.Update(currentBookingDetail);
            customerService.Update(currentBookingDetail.BookingReservation.Customer);
            bookingReservationService.Update(currentBookingDetail.BookingReservation);
            roomInfomationService.Update(currentBookingDetail.Room);
            LoadBookingDetails();
            ResetInput();
        }


        public void DeleteBookingReservation()
        {
            bookingReservationService.Delete(currentBookingReservation.BookingReservationId);
            LoadBookingReservation();
            ResetInput();
        }

        public void DeleteBookingDetail()
        {
            bookingDetailService.Delete(currentBookingDetail.BookingReservationId, currentBookingDetail.RoomId);
            LoadBookingDetails();
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
