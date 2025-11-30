namespace BLE_vaegt_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("WelcomePage", typeof(BLE_vaegt_app.Pages.WelcomePage));
            Routing.RegisterRoute("mainmenu", typeof(BLE_vaegt_app.Pages.MenuPage));
            Routing.RegisterRoute("info", typeof(BLE_vaegt_app.Pages.InfoPage));
            Routing.RegisterRoute("skema", typeof(BLE_vaegt_app.Pages.SkemaPage));
            Routing.RegisterRoute("logoversigt", typeof(BLE_vaegt_app.Pages.LogSkema));
            Routing.RegisterRoute("MainPage", typeof(BLE_vaegt_app.MainPage));
         
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            // Ryd globalt
            GlobalData.Navn = string.Empty;
            GlobalData.Cpr = string.Empty;



            // Naviger tilbage til login
            await Shell.Current.GoToAsync("///LoginPage");
        }

    }
}



