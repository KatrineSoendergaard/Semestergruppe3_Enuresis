namespace BLE_vaegt_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            // Indlæser data aynskront ved app start
            Task.Run(async () => await GlobalData.LoadMeasurements());

            // Sætter AppShell som hovedsiden(navigation shell)
            // AppShell indeholder navigation-menuer og definerer alle app-sider
            MainPage = new AppShell();
        }
    }
}