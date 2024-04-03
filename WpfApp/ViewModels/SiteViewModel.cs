using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using WpfApp.Data;
using WpfApp.Utilities;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class SiteViewModel : ViewModelBase
    {
        private readonly DataEntities _DataEntities;
        private ObservableCollection<Site> _Site;
        private ObservableCollection<User> _User;
        private Site _SelectedSite;

        public SiteViewModel()
        {
            _DataEntities = new DataEntities();

            RefreshData();

            ShowCommand = new ViewModelCommand(Show);
            RefreshCommand = new ViewModelCommand(Refresh);
            SaveCommand = new ViewModelCommand(Save);
            UpdateCommand = new ViewModelCommand(Update);
            DeleteCommand = new ViewModelCommand(Delete);
        }

        private void RefreshData()
        {
            var userInstance = UserInstance.Instance;
            if (userInstance.IsSuperAdmin)
            {
                Users = new ObservableCollection<User>(_DataEntities.Users);
                Sites = new ObservableCollection<Site>(_DataEntities.Sites);
            }
            else
            {
                Users = new ObservableCollection<User>
                {
                    userInstance.User
                };
                Sites = new ObservableCollection<Site>(userInstance.User.Sites);
            }
            SelectedSite = new Site();
        }

        private void Show(object obj)
        {
            if (SelectedSite.ID != 0)
            {
                Views.RegisteredEquipment dlg = new Views.RegisteredEquipment(SelectedSite);
                dlg.Owner = Application.Current.MainWindow;
                dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dlg.ShowDialog();
            }
        }

        private void Refresh(object obj)
        {
            RefreshData();
        }

        private void Save(object obj)
        {
            if (SelectedSite.ID == 0)
            {
                if (AreValidEntries())
                {
                    _DataEntities.Sites.Add(SelectedSite);
                    _DataEntities.SaveChanges();

                    RefreshData();

                    MessageBox.Show("Site successfully created!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please fill-out all the details!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Update(object obj)
        {
            if (SelectedSite.ID != 0)
            {
                if (AreValidEntries())
                {
                    _DataEntities.SaveChanges();

                    RefreshData();

                    MessageBox.Show("Site updated!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please fill-out all the details!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Delete(object obj)
        {
            if (SelectedSite.ID != 0)
            {
                _DataEntities.Sites.Remove(SelectedSite);
                _DataEntities.SaveChanges();

                RefreshData();

                MessageBox.Show("Site deleted!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool AreValidEntries()
        {
            return !string.IsNullOrEmpty(SelectedSite.Description)
                && SelectedSite.UserID != 0 ? true : false;
        }

        public ObservableCollection<Site> Sites
        {
            get => _Site;
            set
            {
                _Site = value;
                OnPropertyChanged(nameof(Sites));
            }
        }

        public ObservableCollection<User> Users
        {
            get => _User;
            set
            {
                _User = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public Site SelectedSite
        {
            get => _SelectedSite;
            set
            {
                _SelectedSite = value;
                OnPropertyChanged(nameof(SelectedSite));
            }
        }

        public bool IsSuperAdmin => UserInstance.Instance.IsSuperAdmin;

        public ICommand ShowCommand { get; }

        public ICommand RefreshCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get; }
    }
}
