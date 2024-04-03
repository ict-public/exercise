using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Resources
{
    /// <summary>
    /// Interaction logic for CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public static readonly DependencyProperty _PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox));

        public CustomPasswordBox()
        {
            InitializeComponent();
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        public string Password
        {
            get
            {
                return (string)GetValue(_PasswordProperty);
            }
            set
            {
                SetValue(_PasswordProperty, value);
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }
    }
}
