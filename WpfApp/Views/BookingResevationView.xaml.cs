using BusinessObjects;
using Services;
using Sevices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for BookingResevationView.xaml
    /// </summary>
    public partial class BookingResevationView : UserControl
    {

        private BookingReservationViewModel viewModel;
        public BookingResevationView()
        {
            InitializeComponent();
            viewModel = new BookingReservationViewModel(new BookingReservationService(), 
                                                        new BookingDetailService(), 
                                                        new CustomerService(),
                                                        new RoomInfomationService());
            DataContext = viewModel;
            viewModel.FilteredBookingDetails = new ObservableCollection<BookingDetail>(viewModel.BookingDetails);
            dgData.SetBinding(DataGrid.ItemsSourceProperty, new Binding("FilteredBookingDetails"));
        }

        private void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(viewModel.CurrentBookingDetail.StartDate > viewModel.CurrentBookingDetail.EndDate)
            {
                bool? Result = new MessageBoxCustom("Start Date must before End Date", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            try
            {
                viewModel.UpdateBookingDetail();
                bool? Result = new MessageBoxCustom("Update Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                BookingDetail bookingDetail = new BookingDetail(viewModel.CurrentBookingDetail.StartDate, viewModel.CurrentBookingDetail.EndDate);
                decimal totalPrice = (viewModel.CurrentBookingDetail.Room.RoomPricePerDay ?? 0) * (viewModel.CurrentBookingDetail.EndDate.DayNumber - viewModel.CurrentBookingDetail.StartDate.DayNumber);
                BookingReservation bookingReservation = new BookingReservation(
                                                    viewModel.CurrentBookingDetail.BookingReservation.BookingDate,
                                                    viewModel.CurrentBookingDetail.BookingReservation.TotalPrice,
                                                    viewModel.CurrentBookingDetail.BookingReservation.Customer,
                                                    viewModel.CurrentBookingDetail.BookingReservation.BookingStatus);
                Customer customer = viewModel.CurrentBookingDetail.BookingReservation.Customer;
                RoomInformation roomInformation = viewModel.CurrentBookingDetail.Room;
                viewModel.SaveBookingDetail(bookingDetail, bookingReservation);
                bool? Result = new MessageBoxCustom("Create Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("Are you sure, You want to close application ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("Are you sure to delete this ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                try
                {
                    viewModel.DeleteBookingDetail();
                    bool? sucess = new MessageBoxCustom("Delete Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
                }
                catch (Exception ex)
                {
                    bool? error = new MessageBoxCustom("Cannot delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            else
            {
                return;
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is BookingDetail selectedBookingDetail)
            {
                viewModel.CurrentBookingDetail = selectedBookingDetail;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SearchQuery = txtSearch.Text;
            viewModel.ApplySearch();
        }
    }
}
