using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer) => CustomerDAO.Add(customer);

        public void DeleteCustomer(int id) => CustomerDAO.Delete(id);

        public List<Customer> GetAll() => CustomerDAO.GetAll();

        public Customer GetCustomerById(int id) => CustomerDAO.GetCustomerById(id);

        public Customer GetCustomerByEmail(string email) => CustomerDAO.GetCustomerByEmail(email);

        public void UpdateCustomer(Customer customer) => CustomerDAO.Update(customer);

        public List<Customer> Search(string keyword) => CustomerDAO.Search(keyword);
    }
}
