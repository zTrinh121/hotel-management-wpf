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
    public class BookingReservationDAO
    {
        public static List<BookingReservation> GetAll()
        {
            var listBookingReservation = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listBookingReservation = context.BookingReservations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingReservation;
        }

        public static List<BookingReservation> GetBookingReservationByCustomerId(int id)
        {
            var bookingReservation = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingReservation = context.BookingReservations.Where(r => r.CustomerId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingReservation;
        }

        public static BookingReservation GetBookingReservationById(int id)
        {
            var bookingReservation = new BookingReservation();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingReservation = context.BookingReservations.FirstOrDefault(r => r.BookingReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingReservation;
        }

        public static void Add(BookingReservation bookingReservation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingReservations.Add(bookingReservation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(BookingReservation bookingReservation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<BookingReservation>(bookingReservation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var bookingReservation = context.BookingReservations.FirstOrDefault(r => r.BookingReservationId == id);
                context.BookingReservations.Remove(bookingReservation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<BookingReservation> GetAllReport(DateOnly startDate, DateOnly endDate)
        {
            using (var context = new FuminiHotelManagementContext())
            {
                var reportData = context.BookingReservations
                                    .Where(br => br.BookingDate >= startDate && br.BookingDate <= endDate)
                                    .OrderByDescending(br => br.BookingDate)
                                    .ToList();

                return reportData;
            }
        }

        public static List<BookingReservation> GetPersonaleeReport(DateOnly startDate, DateOnly endDate)
        {
            using (var context = new FuminiHotelManagementContext())
            {
                var reportData = context.BookingReservations
                                    .Include(br => br.Customer)
                                    .Where(br => br.BookingDate >= startDate && br.BookingDate <= endDate)
                                    .OrderByDescending(br => br.BookingDate)
                                    .ToList();

                return reportData;
            }
        }



    }

}
