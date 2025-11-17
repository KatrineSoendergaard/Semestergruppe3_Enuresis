namespace BLE_vaegt_app.Pages;
using System;
using Microsoft.Maui.Controls;


// Denne attribut gør, at "userName" fra navigationen sættes ind i egenskaben UserName
[QueryProperty(nameof(UserName), "userName")]
public partial class WelcomePage : ContentPage
{


    public WelcomePage()
    {
        InitializeComponent(); // Henter XAML-elementerne (f.eks. WelcomeLabel)
    }

    // Property, som modtager brugernavnet fra LoginPage
    private string userName;
    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            // Når UserName ændres, opdateres labelen på skærmen
            if (WelcomeLabel != null)
                WelcomeLabel.Text = $"Velkommen, {userName}!";
        }



    }

    //public WelcomePage()
    //{
    //    InitializeComponent();

    //    // Prøv FindByName for at se, om elementet er tilgængeligt
    //    var lbl = this.FindByName<Label>("WelcomeLabel");
    //    if (lbl == null)
    //    {
    //        System.Diagnostics.Debug.WriteLine("WelcomeLabel blev IKKE fundet via FindByName!");
    //    }
    //    else
    //    {
    //        System.Diagnostics.Debug.WriteLine("WelcomeLabel fundet — tekst før: " + lbl.Text);
    //        lbl.Text = "Velkommen (test)"; // prøv at opdatere teksten
    //    }

    //}


    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // Navigér videre til hovedmenuen
        await Shell.Current.GoToAsync("//mainmenu");
    }
}

