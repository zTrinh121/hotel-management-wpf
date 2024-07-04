using BusinessObjects;
using Microsoft.Identity.Client;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        public static List<Customer> GetAll()
        {
            var listCustomers = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listCustomers = context.Customers.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCustomers;
        }

        public static Customer GetCustomerById(int id)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public static void Add(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch( Exception ex ) 
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static void Delete(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);
                var bookingReservation = context.BookingReservations.Where(b => b.CustomerId == customer.CustomerId).ToList();
                if(bookingReservation.Count > 0)
                {
                    customer.CustomerStatus = 2;
                }
                else
                {
                    context.Customers.Remove(customer);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void Update(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Customer GetCustomerByEmail(string email)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.EmailAddress == email);
        }

        public static List<Customer> Search(string searchTerm)
        {
            using var context = new FuminiHotelManagementContext();
            var results = context.Customers
                .Where(r => r.CustomerFullName.Contains(searchTerm) || r.EmailAddress.Contains(searchTerm))
                .ToList();

            return new List<Customer>(results);
        }
    }
}
