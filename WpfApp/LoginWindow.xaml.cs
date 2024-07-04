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
using System.Windows.Shapes;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel loginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel(new CustomerService());
            DataContext = loginViewModel;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginViewModel.Login(txtEmail.Text, txtPass.Password);
            }
            catch (Exception ex)
            {
                bool? result = new MessageBoxCustom(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("Are you sure, You want to close application ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
