using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//mainmenu");
        }
    }
}