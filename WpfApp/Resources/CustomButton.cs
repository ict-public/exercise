using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Resources
{
    public class CustomButton : RadioButton
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }
    }
}
