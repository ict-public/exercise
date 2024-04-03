using System.Windows.Input;
using System.Windows;
using WpfApp.Utilities;
using WpfApp.Data;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _CurrentView;
        private bool _IsViewVisible = true;

        public MainViewModel()
        {
            CurrentView = new HomeViewModel();

            HomeCommand = new ViewModelCommand(Home);
            UserCommand = new ViewModelCommand(User);
            SiteCommand = new ViewModelCommand(Site);
            EquipmentCommand = new ViewModelCommand(Equipment);
            LogoutCommand = new ViewModelCommand(Logout);
        }

        private void Home(object obj) => CurrentView = new HomeViewModel();

        private void User(object obj) => CurrentView = new UserViewModel();

        private void Site(object obj) => CurrentView = new SiteViewModel();

        private void Equipment(object obj) => CurrentView = new EquipmentViewModel();

        private void Logout(object obj)
        {
            if (MessageBox.Show("Do you want to logout?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                IsViewVisible = false;
            }
        }

        public object CurrentView
        {
            get { return _CurrentView; }
            set
            {
                _CurrentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public bool IsViewVisible
        {
            get { return _IsViewVisible; }
            set { _IsViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        public bool IsSuperAdmin => UserInstance.Instance.IsSuperAdmin;

        public ICommand HomeCommand { get; }

        public ICommand UserCommand { get; }

        public ICommand SiteCommand { get; }

        public ICommand EquipmentCommand { get; }

        public ICommand LogoutCommand { get; }
    }
}
