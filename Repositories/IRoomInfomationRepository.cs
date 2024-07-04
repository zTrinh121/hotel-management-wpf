using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomInfomationRepository
    {
        List<RoomInformation> GetAll();
        RoomInformation GetRoomInfomationById(int id);
        void SaveRoomInfomation(RoomInformation roomInformation);
        void UpdateRoomInfomation(RoomInformation roomInformation);
        void DeleteRoomInfomation(int id);
        List<RoomInformation> Search(string keyword);
    }
}
