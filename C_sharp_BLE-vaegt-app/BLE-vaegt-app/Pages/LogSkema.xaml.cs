////using Microsoft.Maui.Control;
//using System.ComponentModel;

//namespace BLE_vaegt_app.Pages;

//public partial class LogSkema : ContentPage
//{

//    public LogSkema()
//    {
//        InitializeComponent();
//    }

//    protected override void OnAppearing()
//    {

//        // Kalder basisklassen (ContentPage) OnAppearing-metode
//        // Dette sikrer, at standardopførsel ved fremvisning stadig udføres
//        base.OnAppearing();

//        //// Tøm tidligere viste målinger i StackLayout
//        //// Dette forhindrer, at gamle data vises flere gange, hvis siden genåbnes
//        //MeasurementsStack.Children.Clear();



//        // Gennemgå alle gemte målinger
//        foreach (var m in GlobalData.Measurements)
//        {
//            MeasurementsStack.Children.Add(new Label
//            {
//                Text =
//                    $"Dag: {m.Dag} | Dato: {DateTime.Now:dd-MM-yyyy}  | Kl. {m.Timestamp:HH:mm} | {m.Type}: {m.Weight} g" +
//                    (m.TypiskDag ? " | Typisk dag" : "") +
//                    (!string.IsNullOrWhiteSpace(m.Kommentar) ? $" | Kommentar: {m.Kommentar}" : ""),
//                FontSize = 14,
//                TextColor = Colors.Black
//            });
//        }


//    }

//}

using Microsoft.Maui.Controls;
using System.ComponentModel;
namespace BLE_vaegt_app.Pages;

public partial class LogSkema : ContentPage
{
    public LogSkema()
    {
        InitializeComponent();
    }

    // Async da den gør brug af await
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Indlæser data fra fil
        // Bruger metoden i GlobalDataklassen
        await GlobalData.LoadMeasurements();

        // Tømmer tidligere viste målinger, sådan de ikke bliver vist igen, igen (dubbleter)
        MeasurementsStack.Children.Clear();

        // Tjek om der er nogle målinger
        if (GlobalData.Measurements.Count == 0)
        {
            MeasurementsStack.Children.Add(new Label
            {
                Text = "Ingen målinger gemt endnu.",
                FontSize = 14,
                TextColor = Colors.Gray
            });
            return;
        }

        // Gennemgå alle gemte målinger
        foreach (var m in GlobalData.Measurements)
        {
            MeasurementsStack.Children.Add(new Label
            {
                Text = $"Dag: {m.Dag} | Dato: {m.Timestamp:dd-MM-yyyy} | Kl. {m.Timestamp:HH:mm} | {m.Type}: {m.Weight} g" +
                       (m.TypiskDag ? " | Typisk dag" : "") +
                       (!string.IsNullOrWhiteSpace(m.Kommentar) ? $" | Kommentar: {m.Kommentar}" : ""),
                FontSize = 14,
                TextColor = Colors.Black,
                Padding = new Thickness(5)
            });
        }
    }
}




