using Microsoft.Maui.Controls;
using System.Formats.Tar;
using System.Text.RegularExpressions;
using BLE_vaegt_app.viewmodel;      //Tilføjes forbindelse til viewmodel
namespace BLE_vaegt_app.Pages;


public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();    //Binding til LoginViewModel();
    }

}
