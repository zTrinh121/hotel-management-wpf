using BusinessObjects;
using Services;
using Sevices;
using System.Windows;
using System.Windows.Controls;
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
            viewModel = new BookingReservationViewModel(new BookingReservationService(), new BookingDetailService());
            DataContext = viewModel;
        }

        private void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                viewModel.UpdateBookingReservation();
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
                BookingReservation bookingReservation = new BookingReservation(
                                                    viewModel.CurrentBookingReservation.BookingDate,
                                                    viewModel.CurrentBookingReservation.TotalPrice,
                                                    viewModel.CurrentBookingReservation.Customer,
                                                    viewModel.CurrentBookingReservation.BookingStatus);
                viewModel.SaveBookingReservation();
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
                    viewModel.DeleteBookingReservation();
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
            if (dgData.SelectedItem is BookingReservation selectedBookingReservation)
            {
                viewModel.CurrentBookingReservation = selectedBookingReservation;
            }
        }
    }
}
