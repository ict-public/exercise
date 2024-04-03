using WpfApp.Data;

namespace WpfApp.Models
{
    sealed class UserInstance
    {
        private static UserInstance _Instance;
        private static object _InstanceLock = new object();
        private UserInstance() { }

        public static UserInstance Instance
        {
            get
            {
                lock (_InstanceLock)
                {
                    if (_Instance == null)
                        _Instance = new UserInstance();

                    return _Instance;
                }
            }
        }

        public User User { get; set; }

        public bool IsSuperAdmin => User.UserTypeID == 1 ? true : false;

        public Site Site { get; set; }
    }
}
