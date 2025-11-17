using Microsoft.Maui.Controls;
namespace BLE_vaegt_app.Pages;

    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        // Gør brug af await derfor skal du bruge async metode
        private async void OnNextClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//mainmenu");
        }
    }
