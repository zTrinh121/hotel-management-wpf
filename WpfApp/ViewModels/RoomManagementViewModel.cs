using BusinessObjects;
using Sevices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp.ViewModels
{
    public class RoomManagementViewModel : INotifyPropertyChanged
    {

        private RoomInformation currentRoomInformation;
        private readonly IRoomInfomationService roomInfomationService;
        private readonly IRoomTypeService roomTypeService;
        private ObservableCollection<RoomInformation> roomInformations;
        private ObservableCollection<RoomType> roomTypes;
        private ObservableCollection<RoomInformation> filteredRoomInformations;
        private string searchQuery;
        public ObservableCollection<string> Statuses { get; set; }



        public RoomManagementViewModel(IRoomInfomationService roomInfomationService, IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
            this.roomInfomationService = roomInfomationService;
            LoadRoomInformations();
            LoadRoomType();
            currentRoomInformation = new RoomInformation();
            Statuses = new ObservableCollection<string> { "Active", "Deleted" };
        }


        public RoomInformation CurrentRoomInformation
        {
            get { return currentRoomInformation; }
            set
            {
                currentRoomInformation = value;
                OnPropertyChanged("currentRoomInformation");
            }
        }

        public ObservableCollection<RoomInformation> RoomInformations
        {
            get
            {
                ObservableCollection<RoomType> roomTypes = RoomTypes;
                foreach (var roomInformation in roomInformations)
                {
                    foreach (var roomType in roomTypes)
                    {
                        if (roomType.RoomTypeId == roomInformation.RoomTypeId)
                        {
                            roomInformation.RoomType = roomType;
                            break;
                        }
                    }
                }
                return roomInformations;
            }
            set
            {
                roomInformations = value;
                OnPropertyChanged();
            }
        }


        public void LoadRoomInformations()
        {
            RoomInformations = new ObservableCollection<RoomInformation>(roomInfomationService.GetAll());
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => roomTypes;
            set
            {
                roomTypes = value;
                OnPropertyChanged();
            }
        }

        public void LoadRoomType()
        {
            RoomTypes = new ObservableCollection<RoomType>(roomTypeService.GetAll());
            //OnPropertyChanged(nameof(RoomTypes));
        }

        public void SaveRoomInfomation()
        {
            var newRoomInformation = new RoomInformation
            {
                RoomNumber = CurrentRoomInformation.RoomNumber,
                RoomDetailDescription = CurrentRoomInformation.RoomDetailDescription,
                RoomMaxCapacity = CurrentRoomInformation.RoomMaxCapacity,
                RoomStatus = CurrentRoomInformation.RoomStatus,
                RoomPricePerDay = CurrentRoomInformation.RoomPricePerDay,
                RoomTypeId = CurrentRoomInformation.RoomTypeId
            };
            roomInfomationService.Add(newRoomInformation);
            LoadRoomInformations();
            LoadRoomType();
            ResetInput();
        }
        
        public ObservableCollection<RoomInformation> FilteredRoomInformations
        {
            get => filteredRoomInformations;
            set
            {
                filteredRoomInformations = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;
                OnPropertyChanged();
            }
        }

        public void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredRoomInformations = new ObservableCollection<RoomInformation>(RoomInformations);
                return;
            }

            var query = RoomInformations.Where(r =>
                r.RoomNumber.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                r.RoomPricePerDay.ToString().Contains(SearchQuery) ||
                r.RoomType.RoomTypeName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
            );

            FilteredRoomInformations = new ObservableCollection<RoomInformation>(query.ToList());
        }


        public void UpdateRoomInfomation()
        {
            roomInfomationService.Update(currentRoomInformation);
            LoadRoomInformations();
            LoadRoomType();
            ResetInput();
        }

        public void DeleteRoomInfomation()
        {
            roomInfomationService.Delete(currentRoomInformation.RoomId);
            LoadRoomInformations();
            LoadRoomType();
            ResetInput();
        }

        public void ResetInput()
        {
            currentRoomInformation = new RoomInformation();
            OnPropertyChanged(nameof(CurrentRoomInformation));
        }

        //Notify 
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
