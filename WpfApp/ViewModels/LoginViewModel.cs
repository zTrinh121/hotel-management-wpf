using Sevices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly CustomerService _customerService;
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public LoginViewModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
                ValidateProperty(value, nameof(Email));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ValidateProperty(value, nameof(Password));
            }
        }

        public void Login(string EmailUser, string Pass)
        {
            var user = _customerService.GetCustomerByEmail(EmailUser);

            if (user != null && user.Password.Equals(Pass))
            {
                if (user.EmailAddress == "admin@FUMiniHotelSystem.com")
                {
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else
                {
                    //var customerWindown = new CustomerWindow(user.CustomerId);
                    var customerWindown = new CustomerWindow();

                    customerWindown.Show();
                }
            }
            else
            {
                AddError(nameof(Email), "Invalid email or password.");
                AddError(nameof(Password), "Invalid email or password.");
            }
        }



        //NotifyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //DataError
        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ValidateProperty(object value, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Email):
                    if (string.IsNullOrWhiteSpace((string)value))
                    {
                        AddError(propertyName, "Customer email cannot be empty.");
                    }
                    else
                    {
                        RemoveError(propertyName, "Customer email cannot be empty.");
                    }
                    break;
                case nameof(Password):
                    if (string.IsNullOrWhiteSpace((string)value))
                    {
                        AddError(propertyName, "Customer password cannot be empty.");
                    }
                    else
                    {
                        RemoveError(propertyName, "Customer password cannot be empty.");
                    }
                    break;
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0)
                {
                    _errors.Remove(propertyName);
                }
                OnErrorsChanged(propertyName);
            }
        }
    }
}
