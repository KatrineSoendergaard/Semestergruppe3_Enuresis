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
            Routing.RegisterRoute("InfoPage", typeof(Pages.InfoPage));
            Routing.RegisterRoute("MenuPage", typeof(Pages.MenuPage));
            Routing.RegisterRoute("SkemaPage", typeof(Pages.SkemaPage));
            Routing.RegisterRoute("LogSkema", typeof(Pages.LogSkema));
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
    }
}
