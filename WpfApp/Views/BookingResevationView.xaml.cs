using Services;
using Sevices;
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

        }

        private void btnCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
