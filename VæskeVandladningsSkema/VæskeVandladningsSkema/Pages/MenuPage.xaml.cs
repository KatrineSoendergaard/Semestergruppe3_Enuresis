using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnDag1Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=1");
        }

        private async void OnDag2Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=2");
        }

        private async void OnDag3Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=3");
        }

        private async void OnOversigtClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("LogSkema");
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}