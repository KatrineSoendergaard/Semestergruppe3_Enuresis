namespace BLE_vaegt_app.Pages;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

public partial class SkemaPage : ContentPage
{
    public SkemaPage()
    {
        InitializeComponent();
    }
        private void InfoIkon_Tapped(object sender, EventArgs e)
        {
            InfoText.IsVisible = !InfoText.IsVisible;
        }

        private void AktivitetInfo_Tapped(object sender, EventArgs e)
        {
            AktivitetInfoText.IsVisible = !AktivitetInfoText.IsVisible;
        }

        private async void OnGemClicked(object sender, EventArgs e)
        {
            string dag = DagEntry?.Text ?? "Ukendt";
            string dato = DatoEntry.Date.ToString("dd-MM-yyyy");
            bool typisk = TypiskDag.IsChecked;

            string tid = TidEntry.Text ?? "";
            string vaeske = VaeskeEntry.Text ?? "";
            string vandladning = VandladningEntry.Text ?? "";
            string ufrivillig = UfrivilligEntry.Text ?? "";
            string aktivitet = AktivitetEntry.Text ?? "";

            string data = $"Dag: {dag}, Dato: {dato}, TypiskDag: {typisk}, Tid: {tid}, Væske: {vaeske}, Vandladning: {vandladning}, Ufrivillig: {ufrivillig}, Aktivitet: {aktivitet}";

            GlobalData.SkemaData.Add(data);

            await DisplayAlert("Gem", "Data gemt!", "OK");

            // Clear fields
            DagEntry.Text = string.Empty;
            DatoEntry.Date = DateTime.Today;
            TypiskDag.IsChecked = false;
            TidEntry.Text = string.Empty;
            VaeskeEntry.Text = string.Empty;
            VandladningEntry.Text = string.Empty;
            UfrivilligEntry.Text = string.Empty;
            AktivitetEntry.Text = string.Empty;

            await Shell.Current.GoToAsync("logoversigt");
        }
    }

//    private void InfoIkon_Tapped(object sender, EventArgs e)
//    {
//        InfoText.IsVisible = !InfoText.IsVisible;
//    }

//    private void AktivitetInfo_Tapped(object sender, EventArgs e)
//    {
//        AktivitetInfoText.IsVisible = !AktivitetInfoText.IsVisible;
//    }

//    // Method called from MainPage to populate Bluetooth data
//    public void SetBluetoothData(string tid, string vaeske)
//    {
//        MainThread.BeginInvokeOnMainThread(() =>
//        {
//            BleTidLabel.Text = tid;
//            BleVaeskeLabel.Text = vaeske;
//            UseBleDataButton.IsEnabled = true;
//        });
//    }

//    // When user clicks "Brug disse værdier"
//    private void OnUseBleDataClicked(object sender, EventArgs e)
//    {
//        TidEntry.Text = BleTidLabel.Text;
//        VaeskeEntry.Text = BleVaeskeLabel.Text;
//    }

//    // Gem-knap klik
//    private async void OnGemClicked(object sender, EventArgs e)
//    {
//        string dag = DagEntry?.Text ?? "Ukendt";
//        string dato = DatoEntry.Date.ToString("dd-MM-yyyy");
//        bool typisk = TypiskDag.IsChecked;

//        string tid = TidEntry.Text ?? "";
//        string vaeske = VaeskeEntry.Text ?? "";
//        string vandladning = VandladningEntry.Text ?? "";
//        string ufrivillig = UfrivilligEntry.Text ?? "";
//        string aktivitet = AktivitetEntry.Text ?? "";

//        string data = $"Dag: {dag}, Dato: {dato}, TypiskDag: {typisk}, Tid: {tid}, Væske: {vaeske}, Vandladning: {vandladning}, Ufrivillig: {ufrivillig}, Aktivitet: {aktivitet}";

//        GlobalData.SkemaData.Add(data);

//        await DisplayAlert("Gem", "Data gemt!", "OK");

//        DagEntry.Text = string.Empty;
//        DatoEntry.Date = DateTime.Today;
//        TypiskDag.IsChecked = false;
//        TidEntry.Text = string.Empty;
//        VaeskeEntry.Text = string.Empty;
//        VandladningEntry.Text = string.Empty;
//        UfrivilligEntry.Text = string.Empty;
//        AktivitetEntry.Text = string.Empty;

//        BleTidLabel.Text = "-";
//        BleVaeskeLabel.Text = "-";
//        UseBleDataButton.IsEnabled = false;

//        await Shell.Current.GoToAsync("logoversigt");
//    }
//}




//NEDENUDER FINDES ALT DEN GAMLE KODE !!!!!!!!

//    private void InfoIkon_Tapped(object sender, EventArgs e)
//        {
//            InfoText.IsVisible = !InfoText.IsVisible; // toggle
//        }

//        private void AktivitetInfo_Tapped(object sender, EventArgs e)
//        {
//            AktivitetInfoText.IsVisible = !AktivitetInfoText.IsVisible;
//        }

//        // Gem-knap klik
//        private async void OnGemClicked(object sender, EventArgs e)
//        {
//            string dag = DagEntry?.Text ?? "Ukendt";
//            string dato = DatoEntry.Date.ToString("dd-MM-yyyy");
//            bool typisk = TypiskDag.IsChecked;

//            // Hent data fra grid
//            string tid = ((Entry)InputGrid.Children[0]).Text ?? "";
//            string vaeske = ((Entry)InputGrid.Children[1]).Text ?? "";
//            string vandladning = ((Entry)InputGrid.Children[2]).Text ?? "";
//            string ufrivillig = ((Entry)InputGrid.Children[3]).Text ?? "";
//            string aktivitet = ((Entry)InputGrid.Children[4]).Text ?? "";

//            // Sammensæt til en streng (eller senere gem i objekt/databse)
//            string data = $"Dag: {dag}, Dato: {dato}, TypiskDag: {typisk}, Tid: {tid}, Væske: {vaeske}, Vandladning: {vandladning}, Ufrivillig: {ufrivillig}, Aktivitet: {aktivitet}";

//            // ✅ Gem data i global liste
//            GlobalData.SkemaData.Add(data);



//            await DisplayAlert("Gem", "Data gemt!", "OK");

//            // Ryd felter
//            DagEntry.Text = string.Empty;


//            // Sæt DatoEntry til dagens dato (eller en anden standarddato)
//            DatoEntry.Date = DateTime.Today;

//            TypiskDag.IsChecked = false;
//            foreach (var child in InputGrid.Children)
//                if (child is Entry entry)
//                    entry.Text = string.Empty;

//            // Naviger til Oversigt af målinger
//            await Shell.Current.GoToAsync("logoversigt");
//        }
//}