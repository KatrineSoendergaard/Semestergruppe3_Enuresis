//namespace BLE_vaegt_app.Pages;
namespace BLE_vaegt_app.Pages;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using DataSkema_Library;

public partial class SkemaPage : ContentPage
{


    public SkemaPage()
    {
        InitializeComponent();
    }

    // Metode der går i gang når der trykket på info knappen
    private void InfoIkon_Tapped(object sender, EventArgs e)
    {
        // Hvis teksten er synlig gøre den usynlig og omvendt
        InfoText.IsVisible = !InfoText.IsVisible;
    }

    // Metode der kører når brugeren ændrer tekst i tidsfeltet
    private void TidEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Konverterer sender til Entry
        var entry = sender as Entry;
        // Rerturnere hvis konverteringen fejler
        if (entry == null) return;

        // Henter tekssten indhold fra Entry felt
        string currentText = entry.Text ?? "";

        // Fjerner alt pånær tal
        string pureDigits = new string(currentText.Where(char.IsDigit).ToArray());

        // Hvis der er mere end 4 tal gør den kun brug af de 4 første
        if (pureDigits.Length > 4)
            pureDigits = pureDigits.Substring(0, 4);


        // Start med uformaterede tekst
        string formattedText = pureDigits;

        // Hvis der mindst 2. 2 tal indsættes kolon ala 07:30
        if (pureDigits.Length >= 2)
        {
            formattedText = pureDigits.Insert(2, ":");
        }

        // Hvis teksten er blevet ændret opdateres Entry-feltet
        if (currentText != formattedText)
        {
            // Udfør UI opdatering på hovedtråd
            Dispatcher.Dispatch(() =>
            {
                // Opdater tekstekfeltet med indskrevne klokkeslæt
                entry.Text = formattedText;

                // Placerer markøren i slutningen af teksten
                entry.CursorPosition = formattedText.Length;
            });
        }
    }

    // Metode der håndtere når der trykkes på "Gem"-knappen
    //Async da den gemmer dataen asynkront 
    private async void OnGemClicked(object sender, EventArgs e)
    {
        // Indsaml alle inputværdierne fra UI-elementerne
        //string dag = DagEntry?.Text ?? "";
        DateTime dato = DatoEntry.Date;
        string tid = ((Entry)InputGrid.Children[0]).Text ?? "";
        string vaeske = ((Entry)InputGrid.Children[1]).Text ?? "";
        string vandladning = ((Entry)InputGrid.Children[2]).Text ?? "";
        string kommentarInput = ((Entry)InputGrid.Children[3]).Text ?? "";
        string ble = BleVaegtEntry?.Text ?? "";
        bool typisk = TypiskDag.IsChecked;

        //// Tjekker at dag og tid er udfyldt
        //if (string.IsNullOrWhiteSpace(dag) || string.IsNullOrWhiteSpace(tid))
        //{
        //    await DisplayAlert("Fejl", "Du skal indtaste Dag og Tidspunkt.", "OK");
        //    return;
        //}

        // Validere at tid har gyldigt format
        if (!TimeSpan.TryParse(tid, out var _))
        {
            await DisplayAlert("Fejl", "Indtast et gyldigt klokkeslæt, fx 07:30", "OK");
            return;
        }

        // Forsøger at kombinere dato og tid til en komplet tidspunkt
        DateTime timestamp;
        try
        {
            timestamp = DateTime.Parse($"{dato:yyyy-MM-dd} {tid}");
        }
        catch
        {
            await DisplayAlert("Fejl", "Tidspunktet kunne ikke tolkes. Brug fx 07:00", "OK");
            return;
        }


        // Opretter en liste til at holde på alle målinger
        var measurements = new List<Measurement>();

        // Tilføjer blevægt hvis udfyldt
        if (!string.IsNullOrWhiteSpace(ble))
        {
            var m = new Measurement("Ble", double.Parse(ble))
            {
                //Dag = dag,
                Timestamp = timestamp,
                TypiskDag = typisk,
                Kommentar = kommentarInput
            };
            measurements.Add(m);
        }

        // Tilføjer Væskeindtag hvis udfyldt
        if (!string.IsNullOrWhiteSpace(vaeske))
        {
            var m = new Measurement("Væske", double.Parse(vaeske))
            {
                //Dag = dag,
                Timestamp = timestamp
            };
            measurements.Add(m);
        }

        // Tilføj vandladning-måling hvis udfyldt
        if (!string.IsNullOrWhiteSpace(vandladning))
        {
            var m = new Measurement("Vandladning", double.Parse(vandladning))
            {
                //Dag = dag,
                Timestamp = timestamp
            };
            measurements.Add(m);
        }

        // Tilføj alle målinger til global liste
        foreach (var m in measurements)
            GlobalData.Measurements.Add(m);

        // Gemmer alle målinger til json string
        await GlobalData.SaveMeasurements();

        // Viser bekræftelsesmeddelelse til brugeren
        await DisplayAlert("Gem", "Data gemt!", "OK");

        // Ryd alle inputfelter for at være klar til nye data
        //DagEntry.Text = string.Empty;
        DatoEntry.Date = DateTime.Now;
        TypiskDag.IsChecked = false;
        BleVaegtEntry.Text = string.Empty;

        // Ryd alle tekstfelter i InputGrid
        foreach (var child in InputGrid.Children)
            if (child is Entry entry)
                entry.Text = string.Empty;

        // Naviger til log-oversigt siden hvor de gemte målinger vises
        await Shell.Current.GoToAsync("logoversigt");
    }
}




