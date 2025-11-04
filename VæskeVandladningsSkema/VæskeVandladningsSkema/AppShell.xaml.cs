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
            Routing.RegisterRoute("MenuPage", typeof(Pages.MenuPage));
            Routing.RegisterRoute("InfoPage", typeof(Pages.InfoPage));
            Routing.RegisterRoute("SkemaPage", typeof(Pages.SkemaPage));
            Routing.RegisterRoute("LogSkema", typeof(Pages.LogSkema));
        }
    }
}
