using System.Linq;
using WpfApp.Data;
using WpfApp.Models;
using WpfApp.Utilities;

namespace WpfApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly User _User;

        public HomeViewModel()
        {
            _User = UserInstance.Instance.User;
        }

        public User User => _User;

        public string Sites => string.Join(", ", _User.Sites.Select(x => x.Description));

        //public string Equipments => string.Join(", ", _User.Equipments.Select(x => x.Description));
    }
}
