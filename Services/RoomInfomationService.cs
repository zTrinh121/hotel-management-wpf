using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public class RoomInfomationService : IRoomInfomationService
    {
        private readonly IRoomInfomationRepository _roomInfomationRepository;

        public RoomInfomationService()
        {
            _roomInfomationRepository = new RoomInfomationRepository();
        }
        public void Add(RoomInformation roomInformation) => _roomInfomationRepository.SaveRoomInfomation(roomInformation);

        public List<RoomInformation> GetAll() => _roomInfomationRepository.GetAll();

        public RoomInformation GetRoomInfomationById(int roomId) => _roomInfomationRepository.GetRoomInfomationById(roomId);

        public void Delete(int roomId) => _roomInfomationRepository.DeleteRoomInfomation(roomId);

        public void Update(RoomInformation roomInformation) => _roomInfomationRepository.UpdateRoomInfomation(roomInformation);

        public List<RoomInformation> Search(string keyword) => _roomInfomationRepository.Search(keyword);
    }
}
