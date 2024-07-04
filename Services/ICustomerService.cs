using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer GetCustomerById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
        Customer GetCustomerByEmail(string email);
        List<Customer> Search(string keyword);
    }
}
