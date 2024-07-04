using BusinessObjects;
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
    /// Interaction logic for CustomerManagemetnView.xaml
    /// </summary>
    public partial class CustomerManagemetnView : UserControl
    {
        private CustomerManagementViewModel viewModel;
        public CustomerManagemetnView()
        {
            InitializeComponent();
            viewModel = new CustomerManagementViewModel(new CustomerService());
            DataContext = viewModel;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer(viewModel.CurrentCustomer.CustomerFullName,
                                                viewModel.CurrentCustomer.Telephone,
                                                viewModel.CurrentCustomer.EmailAddress,
                                                viewModel.CurrentCustomer.CustomerBirthday,
                                                viewModel.CurrentCustomer.CustomerStatus,
                                                viewModel.CurrentCustomer.Password);
                viewModel.SaveCustomer();
                bool? Result = new MessageBoxCustom("Create Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.UpdateCustomer();
                bool? Result = new MessageBoxCustom("Update Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("Are you sure to delete this ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                try
                {
                    viewModel.DeleteCustomer();
                    bool? sucess = new MessageBoxCustom("Delete Successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
                }
                catch (Exception ex)
                {
                    bool? error = new MessageBoxCustom("Cannot delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }else
            {
                return;
            }
            
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
            if (dgData.SelectedItem is Customer selectedCustomer)
            {
                viewModel.CurrentCustomer = selectedCustomer;
            }
        }
    }
}
