using Azure.Core;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingDetailDAO
    {
        public static List<BookingDetail> GetAll()
        {
            var listBookingDetail = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listBookingDetail = context.BookingDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingDetail;
        }

        public static BookingDetail GetBookingDetailByBookingReservationId(int id)
        {
            var bookingDetail = new BookingDetail();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetail = context.BookingDetails.FirstOrDefault(r => r.BookingReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetail;
        }

        public static List<BookingDetail> GetBookingDetailByRoomId(int id)
        {
            var bookingDetail = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetail = context.BookingDetails.Where(r => r.RoomId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetail;
        }

        public static void Add(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<BookingDetail>(bookingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Delete(int bookingReservationId, int roomId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var bookingDetail = context.BookingDetails.FirstOrDefault(r => r.BookingReservationId == bookingReservationId 
                                                                        && r.RoomId == roomId);
                context.BookingDetails.Remove(bookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }

}
