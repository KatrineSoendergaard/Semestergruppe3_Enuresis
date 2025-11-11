using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class SkemaPage : ContentPage
    {
        public SkemaPage()
        {
            InitializeComponent();
        }


        private void InfoIkon_Tapped(object sender, EventArgs e)
        {
            InfoText.IsVisible = !InfoText.IsVisible; // toggle
        }
        private async void OnGemClicked(object sender, EventArgs e)
        {
            bool erUtæt = TypiskDag.IsChecked;
            await Shell.Current.GoToAsync("//logoversigt");
        }
    }
}