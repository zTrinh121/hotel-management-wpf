using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public interface IRoomInfomationService
    {
        List<RoomInformation> GetAll();
        RoomInformation GetRoomInfomationById(int roomId);
        void Add(RoomInformation roomInformation);
        void Delete(int roomId);
        void Update(RoomInformation roomInformation);
        List<RoomInformation> Search(string keyword);
    }
}
