using BusinessObjects;
using Services;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public PersonalReport()
        {
            InitializeComponent();
            viewModel = new ReportViewModel(new BookingReservationService(), new BookingDetailService(), 18);
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
