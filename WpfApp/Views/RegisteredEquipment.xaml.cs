using System.Data;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for RegisteredEquipment.xaml
    /// </summary>
    public partial class RegisteredEquipment : Window
    {
        private readonly Data.Site _Site;

        public RegisteredEquipment(Data.Site site)
        {
            InitializeComponent();
            DataContext = new ViewModels.RegisteredEquipmentViewModel(site);

            //UserInstance.Instance.Site = site;
            //_Site = site;
            txtSiteID.Text = site.ID.ToString();
            txtSite.Text = site.Description;
        }
    }
}
