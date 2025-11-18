namespace BLE_vaegt_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register all navigation routes with full namespace
            Routing.RegisterRoute("WelcomePage", typeof(BLE_vaegt_app.Pages.WelcomePage));
            Routing.RegisterRoute("mainmenu", typeof(BLE_vaegt_app.Pages.MenuPage));
            Routing.RegisterRoute("info", typeof(BLE_vaegt_app.Pages.InfoPage));
            Routing.RegisterRoute("skema", typeof(BLE_vaegt_app.Pages.SkemaPage));
            Routing.RegisterRoute("logoversigt", typeof(BLE_vaegt_app.Pages.LogSkema));
            Routing.RegisterRoute("MainPage", typeof(BLE_vaegt_app.MainPage));
        }
    }
}

