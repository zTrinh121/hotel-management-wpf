using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomInfomationRepository : IRoomInfomationRepository
    {
        public void DeleteRoomInfomation(int id) => RoomInfomationDAO.Delete(id);

        public List<RoomInformation> GetAll() => RoomInfomationDAO.GetAll();

        public RoomInformation GetRoomInfomationById(int id) => RoomInfomationDAO.GetRoomInfomationById(id);

        public void SaveRoomInfomation(RoomInformation roomInformation) => RoomInfomationDAO.Add(roomInformation);

        public List<RoomInformation> Search(string keyword) => RoomInfomationDAO.Search(keyword);

        public void UpdateRoomInfomation(RoomInformation roomInformation) => RoomInfomationDAO.Update(roomInformation);
    }
}
