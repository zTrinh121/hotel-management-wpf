using Microsoft.Identity.Client.NativeInterop;
using Services;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public int CustomerId;
        public CustomerWindow(int customerId)
        {
            InitializeComponent();
            //CustomerId = customerId;
            CustomerId = customerId;
        }

        private void AccountManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new PersonalProfileViewModel(new CustomerService(), CustomerId);
        }

        private void Booking_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(CustomerId);
            DataContext = new ReportViewModel(new BookingReservationService(), new BookingDetailService(), new CustomerService(), new RoomInfomationService(), CustomerId);
        }
    }
}
