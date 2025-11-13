using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Sørg for at felterne er tomme hver gang siden vises
        protected override void OnAppearing()
        {
            base.OnAppearing();

            NavnEntry.Text = string.Empty;
            CprEntry.Text = string.Empty;
        }


        // Kun bogstaver i Navn
        private void NavnEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null) return;

            string oldText = entry.Text ?? "";
            string newText = Regex.Replace(oldText, @"[^a-zA-ZæøåÆØÅ\s]", ""); // kun bogstaver og mellemrum

            if (newText != oldText)
            {
                int cursorPos = entry.CursorPosition;
                entry.Text = newText;
                entry.CursorPosition = cursorPos > newText.Length ? newText.Length : cursorPos;
            }
        }


        // CPR: kun tal, præcis 10 cifre
        private void CprEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null) return;

            string oldText = entry.Text ?? "";
            string newText = Regex.Replace(oldText, @"[^0-9]", ""); // fjern alt der ikke er tal

            if (newText.Length > 10)
                newText = newText.Substring(0, 10); // max 10 cifre

            if (newText != oldText)
            {
                int cursorPos = entry.CursorPosition;
                entry.Text = newText;
                entry.CursorPosition = cursorPos > newText.Length ? newText.Length : cursorPos;
            }
        }

        // Melder hvilken type fejl brugeren har lavet, og går videre hvis både CPR og Navn er indtastet korrekt
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NavnEntry.Text))
            {
                await DisplayAlert("Fejl", "Navn skal udfyldes.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CprEntry.Text) || CprEntry.Text.Length != 10)
            {
                await DisplayAlert("Fejl", "CPR skal være præcis 10 cifre. Prøv igen.", "OK");
                CprEntry.Text = string.Empty;
                return;
            }

            // Gem globalt
            GlobalData.Navn = NavnEntry.Text;
            GlobalData.Cpr = CprEntry.Text;

            // Naviger til WelcomePage
            await Shell.Current.GoToAsync($"WelcomePage?userName={NavnEntry.Text}");
        }

    }
}