using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnInfoClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//info");
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

        private async void OnDag4Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=4");
        }

        private async void OnDag5Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=5");
        }

        private async void OnDag6Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=6");
        }
        private async void OnDag7Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SkemaPage?dag=7");
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