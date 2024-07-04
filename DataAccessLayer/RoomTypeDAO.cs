using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        public static List<RoomType> GetAll()
        {
            var listRoomTypes = new List<RoomType>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listRoomTypes = context.RoomTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRoomTypes;
        }
    }
}
