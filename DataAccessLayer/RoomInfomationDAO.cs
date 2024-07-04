using Azure.Core;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomInfomationDAO
    {
        public static List<RoomInformation> GetAll()
        {
            var listRoomInfomation = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listRoomInfomation = context.RoomInformations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRoomInfomation;
        }

        public static RoomInformation GetRoomInfomationById(int id)
        {
            var roomInfomation = new RoomInformation();
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInfomation = context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInfomation;
        }

        //public static List<RoomInformation> GetAllByRoomTypeId(int roomTypeId)
        //{
        //    var listRoomInfomation = new List<RoomInformation>();
        //    try
        //    {
        //        using var context = new HotelManagementContext();
        //        listRoomInfomation = context.RoomInformations.Where(r => r.RoomTypeId == roomTypeId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return listRoomInfomation;
        //}

        public static void Add(RoomInformation roomInfomation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.RoomInformations.Add(roomInfomation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(RoomInformation roomInfomation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<RoomInformation>(roomInfomation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var roomInfomation = context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
                var bookingDetail = context.BookingDetails.Where(b => b.RoomId == id).ToList();
                if(bookingDetail.Count > 0) 
                {
                    roomInfomation.RoomStatus = 2;
                }
                else
                {
                    context.RoomInformations.Remove(roomInfomation);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<RoomInformation> Search(string searchTerm)
        {
            using var context = new FuminiHotelManagementContext();
            var results = context.RoomInformations
                .Where(r => r.RoomNumber.Contains(searchTerm) || r.RoomDetailDescription.Contains(searchTerm))
                .ToList();

            return new List<RoomInformation>(results);
        }

    }

}
