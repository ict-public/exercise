using System.Windows;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }
    }
}
