using Services;
using Sevices;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void RoomManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomManagementViewModel(new RoomInfomationService(), new RoomTypeService());
        }

        
        private void AccountManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CustomerManagementViewModel(new CustomerService());
        }

        private void Report_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AdminReportViewModel(new BookingReservationService());
        }
        

        private void Booking_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BookingReservationViewModel(new BookingReservationService(), new BookingDetailService(), new CustomerService(), new RoomInfomationService());
        }
    }


}
