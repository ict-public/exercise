using System.Windows;
using System.Windows.Input;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainViewModel();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MainPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainView mainView = sender as MainView;
            if (!mainView.IsVisible && mainView.IsLoaded)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                mainView.Close();
            }
        }
    }
}
