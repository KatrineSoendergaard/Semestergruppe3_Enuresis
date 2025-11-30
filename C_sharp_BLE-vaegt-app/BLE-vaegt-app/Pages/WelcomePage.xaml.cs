namespace BLE_vaegt_app.Pages;
using System;
using Microsoft.Maui.Controls;


// Denne attribut gør, at "userName" fra navigationen sættes ind i egenskaben UserName
// Fx. JENS JENSEN sættes til brugernavnet til Jens Jensen 
[QueryProperty(nameof(UserName), "userName")]
public partial class WelcomePage : ContentPage
{


    public WelcomePage()
    {
        InitializeComponent(); // Henter XAML-elementerne (f.eks. WelcomeLabel)
    }

    // Property, som modtager brugernavnet fra LoginPage
    // Gemmer det internt = private
    private string userName;

    // Property som modtager brugernavnet fra navigationen via QueryProperty
    public string UserName
    {
        // Returnerer det gemte brugernavn
        get => userName;
        
        // Kører når brugernavnet bliver sat
        set
        {
            // Gemmer den nye værdi i private field
            userName = value;
            
            // Når UserName ændres, opdateres labelen på skærmen
            if (WelcomeLabel != null) // Tjekker om welcomeLabel findes
                WelcomeLabel.Text = $"Velkommen, {userName}!";
        }
        
    }

    // Metode der styrer "Forsæt" -knappen
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // Navigér videre til hovedmenuen
        await Shell.Current.GoToAsync("mainmenu");
    }
}

