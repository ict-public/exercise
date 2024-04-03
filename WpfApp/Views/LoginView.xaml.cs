using System.Windows;
using System.Windows.Input;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new ViewModels.LoginViewModel();
            txtUsername.Focus();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LoginPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoginView loginView = sender as LoginView;
            if (!loginView.IsVisible && loginView.IsLoaded)
            {
                MainView mainView = new MainView();
                mainView.Show();
                loginView.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
