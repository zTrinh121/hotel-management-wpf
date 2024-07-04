using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        public void Add(Customer customer) => _customerRepository.AddCustomer(customer);

        public void Delete(int id) => _customerRepository.DeleteCustomer(id);

        public List<Customer> GetAll() => _customerRepository.GetAll();

        public Customer GetCustomerById(int id) => _customerRepository.GetCustomerById(id);

        public Customer GetCustomerByEmail(string email) => _customerRepository.GetCustomerByEmail(email);

        public void Update(Customer customer) => _customerRepository.UpdateCustomer(customer);

        public List<Customer> Search(string keyword) => _customerRepository.Search(keyword);
    }
}
