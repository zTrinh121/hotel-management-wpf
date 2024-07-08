using BusinessObjects;
using Services;
using Sevices;
using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for PersonalReport.xaml
    /// </summary>
    public partial class PersonalReport : UserControl
    {
        ReportViewModel viewModel;
        int CustomerId;

        public PersonalReport()
        {
            InitializeComponent();
        }
        public PersonalReport(int customerId)
        {
            CustomerId = customerId;
            viewModel = new ReportViewModel(new BookingReservationService(), 
                                            new BookingDetailService(), 
                                            new CustomerService(),
                                            new RoomInfomationService(),
                                            CustomerId);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("Are you sure, You want to close application ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                Application.Current.Shutdown();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
