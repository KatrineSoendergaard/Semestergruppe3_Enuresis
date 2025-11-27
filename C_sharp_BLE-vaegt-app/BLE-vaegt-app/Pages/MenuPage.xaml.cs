namespace BLE_vaegt_app.Pages;

public partial class MenuPage : ContentPage
{
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnInfoClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("info"); // "//" betyder, at du navigerer til en top-level route (altså en FlyoutItem)
        }

        private async void OnSkemaClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("skema");
        }

        private async void OnOversigtClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("logoversigt");
        }

        private async void OnBluetoothClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("MainPage");
        }
        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {

        // Ryd globalt
        GlobalData.Reset();

        // Naviger tilbage til login
        await Shell.Current.GoToAsync("///LoginPage");
    }

}