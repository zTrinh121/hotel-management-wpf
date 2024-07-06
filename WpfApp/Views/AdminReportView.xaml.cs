
using System.Windows;
using System.Windows.Controls;
using Services;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for AdminReportView.xaml
    /// </summary>
    public partial class AdminReportView : UserControl
    {
        public AdminReportView()
        {
            InitializeComponent();
            DataContext = new AdminReportViewModel(new BookingReservationService());
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AdminReportViewModel;

            DateTime? selectedStartDate = startDate.SelectedDate;
            DateTime? selectedEndDate = endDate.SelectedDate;

            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                DateOnly startDateOnly = DateOnly.FromDateTime(selectedStartDate.Value);
                DateOnly endDateOnly = DateOnly.FromDateTime(selectedEndDate.Value);

                // Call method on view model to filter report by date range
                viewModel?.FilterReportByDateRange(startDateOnly, endDateOnly);
            }
            else
            {
                MessageBox.Show("Please select both start and end dates.");
            }
        }
    }
}
