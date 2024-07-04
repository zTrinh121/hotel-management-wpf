
using BusinessObjects;
using Sevices;
using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : UserControl
    {
        private RoomManagementViewModel viewModel;
        public RoomManagement()
        {
            InitializeComponent();
            viewModel = new RoomManagementViewModel(new RoomInfomationService(), new RoomTypeService());
            DataContext = viewModel;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomInformation roomInformation = new RoomInformation(
                                                    viewModel.CurrentRoomInformation.RoomNumber,
                                                    viewModel.CurrentRoomInformation.RoomDetailDescription,
                                                    viewModel.CurrentRoomInformation.RoomMaxCapacity,
                                                    viewModel.CurrentRoomInformation.RoomStatus,
                                                    viewModel.CurrentRoomInformation.RoomPricePerDay,
                                                    viewModel.CurrentRoomInformation.RoomTypeId);
                viewModel.SaveRoomInfomation();
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
                viewModel.UpdateRoomInfomation();
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
                    viewModel.DeleteRoomInfomation();
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

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is RoomInformation selectedRoomInformation)
            {
                viewModel.CurrentRoomInformation = selectedRoomInformation;
            }
        }
    }
}
