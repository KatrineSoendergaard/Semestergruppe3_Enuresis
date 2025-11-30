namespace BLE_vaegt_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            // INDLÆS DATA ASYNKRONT VED APP START
            Task.Run(async () => await GlobalData.LoadMeasurements()); // <-- TILFØJ DENNE LINJE

            MainPage = new AppShell();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}
    }
}