using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using WpfApp.Data;
using WpfApp.Utilities;
using System.Linq;

namespace WpfApp.ViewModels
{
    public class RegisteredEquipmentViewModel : ViewModelBase
    {
        private readonly DataEntities _DataEntities;
        private ObservableCollection<RegisteredEquipment> _RegisteredEquipment;
        private ObservableCollection<Equipment> _Equipments;
        private ObservableCollection<Site> _Sites;
        private Site _SelectedSite;
        private RegisteredEquipment _SelectedEquipment;

        public RegisteredEquipmentViewModel(Site site)
        {
            _DataEntities = new DataEntities();

            SelectedSite = site;
            RefreshData();

            RefreshCommand = new ViewModelCommand(Refresh);
            SaveCommand = new ViewModelCommand(Save);
            DeleteCommand = new ViewModelCommand(Delete);
        }

        private void RefreshData()
        {
            var regEquipments = _DataEntities.RegisteredEquipments.Select(x => x.EquipmentID).ToList();

            RegisteredEquipments = new ObservableCollection<RegisteredEquipment>(_DataEntities.RegisteredEquipments.Where(x => x.SiteID == SelectedSite.ID));
            Equipments = new ObservableCollection<Equipment>(_DataEntities.Equipments.Where(x => !regEquipments.Contains(x.ID) && x.UserID == SelectedSite.UserID));
            Sites = new ObservableCollection<Site>(_DataEntities.Sites);
            SelectedEquipment = new RegisteredEquipment();
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
                    _DataEntities.RegisteredEquipments.Add(new RegisteredEquipment
                    {
                        SiteID = SelectedSite.ID,
                        EquipmentID = SelectedEquipment.ID
                    });
                    _DataEntities.SaveChanges();

                    RefreshData();

                    MessageBox.Show("Equipment registered successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                _DataEntities.RegisteredEquipments.Remove(SelectedEquipment);
                _DataEntities.SaveChanges();

                RefreshData();

                MessageBox.Show("Equipment deleted!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool AreValidEntries()
        {
            return SelectedEquipment.SiteID != 0
                && SelectedEquipment.EquipmentID != 0 ? true : false;
        }

        public ObservableCollection<RegisteredEquipment> RegisteredEquipments
        {
            get => _RegisteredEquipment;
            set
            {
                _RegisteredEquipment = value;
                OnPropertyChanged(nameof(RegisteredEquipments));
            }
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

        public ObservableCollection<Site> Sites
        {
            get => _Sites;
            set
            {
                _Sites = value;
                OnPropertyChanged(nameof(Sites));
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

        public RegisteredEquipment SelectedEquipment
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

        public ICommand DeleteCommand { get; }
    }
}
