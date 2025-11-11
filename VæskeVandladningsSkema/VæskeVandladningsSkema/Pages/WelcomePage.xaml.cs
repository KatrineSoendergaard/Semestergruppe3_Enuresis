using System;
using Microsoft.Maui.Controls;
namespace VaeskeVandladningsSkema.Pages;

public partial class WelcomePage : ContentPage
{

    public WelcomePage()
    {
        InitializeComponent();

    }


    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // Navigér videre til hovedmenuen
        await Shell.Current.GoToAsync("//mainmenu");
    }
}