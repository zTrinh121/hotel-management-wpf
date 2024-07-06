using BusinessObjects;
using Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    public class PersonalProfileViewModel : INotifyPropertyChanged
    {
        private Customer currentCustomer;
        private readonly ICustomerService customerService;
        public ObservableCollection<string> Statuses { get; set; }


        public PersonalProfileViewModel(ICustomerService customerService, int CustomerId)
        {
            this.customerService = customerService;
            
            currentCustomer = LoadCustomer(CustomerId); ;
            Statuses = new ObservableCollection<string> { "Active", "Deleted" };
        }

        public Customer LoadCustomer(int CustomerId)
        {
            var customer = customerService.GetCustomerById(CustomerId);
            CurrentCustomer = customer;
            return customer;
        }

        public Customer CurrentCustomer
        {
            get => currentCustomer;
            set
            {
                currentCustomer = value;
                OnPropertyChanged();
            }
        }


        public void SaveCustomer()
        {

            var newCustomer = new Customer
            {
                CustomerFullName = CurrentCustomer.CustomerFullName,
                Telephone = CurrentCustomer.Telephone,
                EmailAddress = CurrentCustomer.EmailAddress,
                CustomerBirthday = CurrentCustomer.CustomerBirthday,
                CustomerStatus = CurrentCustomer.CustomerStatus ?? 1,
                Password = CurrentCustomer.Password
            };
            customerService.Add(newCustomer);

        }

        public void UpdateCustomer()
        {
            customerService.Update(currentCustomer);
            OnPropertyChanged(nameof(CurrentCustomer));

        }

        public void DeleteCustomer()
        {
            customerService.Delete(currentCustomer.CustomerId);
            
        }

        public void ResetInput()
        {
            currentCustomer = new Customer();
        }

        //NotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
