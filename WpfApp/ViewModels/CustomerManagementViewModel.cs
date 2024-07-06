using BusinessObjects;
using Sevices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp.ViewModels
{
    public class CustomerManagementViewModel : INotifyPropertyChanged
    {
        private Customer currentCustomer;
        private readonly ICustomerService customerService;
        private ObservableCollection<Customer> customers;
        private string searchQuery;
        private ObservableCollection<Customer> filteredCustomers;
        public ObservableCollection<string> Statuses { get; set; }

        public CustomerManagementViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
            LoadCustomers();
            currentCustomer = new Customer();
            Statuses = new ObservableCollection<string> { "Active", "Deleted" };
        }

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public void LoadCustomers() => Customers = new ObservableCollection<Customer>(customerService.GetAll());

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Customer> FilteredCustomers
        {
            get => filteredCustomers;
            set
            {
                filteredCustomers = value;
                OnPropertyChanged();
            }
        }

        public void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredCustomers = new ObservableCollection<Customer>(Customers);
                return;
            }

            var query = Customers.Where(c =>
                c.CustomerFullName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                c.Telephone.Contains(SearchQuery) ||
                c.EmailAddress.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                c.CustomerBirthday.ToString().Contains(SearchQuery)
            );

            FilteredCustomers = new ObservableCollection<Customer>(query.ToList());
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
            LoadCustomers();
            ResetInput();
        }

        public void UpdateCustomer()
        {
            customerService.Update(currentCustomer);
            LoadCustomers();
            ResetInput();
        }

        public void DeleteCustomer()
        {
            customerService.Delete(currentCustomer.CustomerId);
            LoadCustomers();
            ResetInput();
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

        public void ResetInput()
        {
            currentCustomer = new Customer();
            OnPropertyChanged(nameof(CurrentCustomer));
        }


        //NotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
