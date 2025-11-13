using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Ekstra: registrer route-navne centralt (valgfrit, men hjælpsomt)
            Routing.RegisterRoute("LoginPage", typeof(Pages.LoginPage));
            Routing.RegisterRoute("info", typeof(Pages.InfoPage));
            Routing.RegisterRoute("mainmenu", typeof(Pages.MenuPage));
            Routing.RegisterRoute("skema", typeof(Pages.SkemaPage));
            Routing.RegisterRoute("logoversigt", typeof(Pages.LogSkema));
            Routing.RegisterRoute("WelcomePage", typeof(Pages.WelcomePage));


        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Target.Location.OriginalString.Contains("LoginPage"))
            {
                this.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
            else
            {
                this.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }

        // Log ud: Rydder global brugerdata og navigerer tilbage til login-siden
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Ryd global data
            GlobalData.Navn = string.Empty;
            GlobalData.Cpr = string.Empty;

            // Naviger tilbage til login
            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}
