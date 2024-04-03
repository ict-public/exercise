using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using WpfApp.Data;
using WpfApp.Models;
using WpfApp.Utilities;
using System;
using System.Windows.Controls;

namespace WpfApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly DataEntities _DataEntities;
        private ObservableCollection<User> _Users;
        private User _User;
        private string _Username;
        private string _Password;
        private bool _IsViewVisible = true;

        public LoginViewModel()
        {
            _DataEntities = new DataEntities();
            Users = new ObservableCollection<User>(_DataEntities.Users);
            LoginCommand = new ViewModelCommand(Login, CanLogin);
            CreateCommand = new ViewModelCommand(Create);
        }

        private bool CanLogin(object obj)
        {
            return (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)) ? false : true;
        }

        private void Login(object obj)
        {
            bool IsUserValid = AreValidLogin();
            if (IsUserValid)
            {
                var instance = UserInstance.Instance;
                instance.User = _User;

                IsViewVisible = false;
            }
            else
            {
                MessageBox.Show("Invalid username/password!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Create(object obj)
        {
            Views.CreateUser dlg = new Views.CreateUser();
            dlg.Owner = Application.Current.MainWindow;
            dlg.ShowDialog();
        }

        private bool AreValidLogin()
        {
            _User = _DataEntities.Users.FirstOrDefault(x => x.Username == _Username && x.Password == Password);

            return _User is null ? false : true;
        }

        public ObservableCollection<User> Users
        {
            get => _Users;
            set
            {
                _Users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool IsViewVisible
        {
            get { return _IsViewVisible; }
            set { _IsViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        public ICommand LoginCommand { get; }

        public ICommand CreateCommand { get; }
    }
}
