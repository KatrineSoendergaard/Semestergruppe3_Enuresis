using BLE_vaegt_app.viewmodel;      //Tilføjes forbindelse til viewmodel
using Microsoft.Maui.Controls;
using System.Formats.Tar;
using System.Text.RegularExpressions;
namespace BLE_vaegt_app.Pages;


public partial class LoginPage : ContentPage
{
    private LoginViewModel viewModel;

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(); //Binding til LoginViewModel();
        InitializeComponent();
        viewModel = (LoginViewModel)BindingContext;
    }


    // Kaldes automatisk når når brugergrænsefladen vises på skærmen
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Kald OnAppearing på viewmodellen
        viewModel?.OnAppearing();
    }
}



