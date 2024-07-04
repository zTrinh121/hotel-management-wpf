using BusinessObjects;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for PersonalProfile.xaml
    /// </summary>
    public partial class PersonalProfile : UserControl
    {
        public PersonalProfileViewModel viewModel;
        public PersonalProfile()
        {
            InitializeComponent();
            viewModel = new PersonalProfileViewModel(new CustomerService(), 18);
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
            }
            else
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
    }
}
