using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfApp.Data;
using WpfApp.Models;
using WpfApp.Utilities;

namespace WpfApp.ViewModels
{
    public class EquipmentViewModel : ViewModelBase
    {
        private readonly DataEntities _DataEntities;
        private ObservableCollection<Equipment> _Equipments;
        private ObservableCollection<User> _Users;
        private ObservableCollection<Data.Condition> _Conditions;
        private Equipment _SelectedEquipment;

        public EquipmentViewModel()
        {
            _DataEntities = new DataEntities();

            RefreshData();

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
                Equipments = new ObservableCollection<Equipment>(_DataEntities.Equipments);
            }
            else
            {
                Users = new ObservableCollection<User>
                {
                    userInstance.User
                };
                Equipments = new ObservableCollection<Equipment>(userInstance.User.Equipments);
            }
            
            Conditions = new ObservableCollection<Data.Condition>(_DataEntities.Conditions);
            SelectedEquipment = new Equipment();
        }

        private void Refresh(object obj)
        {
            RefreshData();
        }

        private void Save(object obj)
        {
            if (SelectedEquipment.ID == 0)
            {
                if (AreValidEntries())
                {
                    _DataEntities.Equipments.Add(SelectedEquipment);
                    _DataEntities.SaveChanges();

                    RefreshData();

                    MessageBox.Show("Equipment successfully created!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please fill-out all the details!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Update(object obj)
        {
            if (SelectedEquipment.ID != 0)
            {
                if (AreValidEntries())
                {
                    _DataEntities.SaveChanges();

                    RefreshData();

                    MessageBox.Show("Equipment updated!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please fill-out all the details!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Delete(object obj)
        {
            if (SelectedEquipment.ID != 0)
            {
                _DataEntities.Equipments.Remove(SelectedEquipment);
                _DataEntities.SaveChanges();

                RefreshData();

                MessageBox.Show("Equipment deleted!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool AreValidEntries()
        {
            return !string.IsNullOrEmpty(SelectedEquipment.SerialNumber)
                && !string.IsNullOrEmpty(SelectedEquipment.Description)
                && SelectedEquipment.ConditionID != 0
                && SelectedEquipment.UserID != 0 ? true : false;
        }

        public ObservableCollection<Equipment> Equipments
        {
            get => _Equipments;
            set
            {
                _Equipments = value;
                OnPropertyChanged(nameof(Equipments));
            }
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

        public ObservableCollection<Data.Condition> Conditions
        {
            get => _Conditions;
            set
            {
                _Conditions = value;
                OnPropertyChanged(nameof(Conditions));
            }
        }

        public Equipment SelectedEquipment
        {
            get => _SelectedEquipment;
            set
            {
                _SelectedEquipment = value;
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }

        public ICommand RefreshCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get; }
    }
}
