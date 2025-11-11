using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
