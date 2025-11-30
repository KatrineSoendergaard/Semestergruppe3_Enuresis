namespace BLE_vaegt_app.Pages;
using DataSkema_Library;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
    }

    private async void OnInfoClicked(object sender, System.EventArgs e)
    {
        await Shell.Current
            .GoToAsync("info"); // Hvis den crasher indsæt (før info) --> "//" betyder, at du navigerer til en top-level route (altså en FlyoutItem)
    }

    // Metode der går til indtast skema måling manuelt
    private async void OnSkemaClicked(object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("skema");
    }

    // Metode der går til oversigten over alle gemte målinger
    private async void OnOversigtClicked(object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("logoversigt");
    }

    // Metode der går til bluetooth siden
    private async void OnBluetoothClicked(object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("MainPage");
    }

    // Metode der går til login side og fjerner sidst indtastet login
    private async void OnLogoutClicked(object sender, System.EventArgs e)
    {

        // Ryd globalt
        GlobalData.Navn = string.Empty;
        GlobalData.Cpr = string.Empty;

        
        // Naviger tilbage til login
        await Shell.Current.GoToAsync("//LoginPage");
    }

}