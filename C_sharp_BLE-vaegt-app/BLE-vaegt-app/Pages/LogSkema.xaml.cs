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

        // ALT NEDENUNDER ER TESTER AF SLETTE KNAP
        foreach (var m in GlobalData.Measurements)
        {
            // Lavet til grid sådan at skraldespand var helt til højre
            var grid = new Grid 
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                Padding = new Thickness(0),
                ColumnSpacing = 5,
                
            };

            grid.Add(new Label
            {
                Text = $"Dag: {m.Dag} | Dato: {m.Timestamp:dd-MM-yyyy} | Kl. {m.Timestamp:HH:mm} | {m.Type}: {m.Weight} g" +
                       (m.TypiskDag ? " | Typisk dag" : "") +
                       (!string.IsNullOrWhiteSpace(m.Kommentar) ? $" | Kommentar: {m.Kommentar}" : ""),
               
                FontSize = 13,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            }, 0, 0);

            var deleteBtn = new ImageButton
            {
                Source = "delete.png",
                WidthRequest = 8,
                HeightRequest = 8,
                Scale = 0.4,
                BackgroundColor = Colors.Transparent
            };

            deleteBtn.Clicked += async (s, e) =>
            {
                GlobalData.Measurements.Remove(m);
                await GlobalData.SaveMeasurements();
                await Navigation.PushAsync(new LogSkema());
            };

            grid.Add(deleteBtn, 1, 0);
            MeasurementsStack.Children.Add(grid);
        }

        // Tester slette knap gammel kode nedenunder
        //// Gennemgå alle gemte målinger
        //foreach (var m in GlobalData.Measurements)
        //{
        //    MeasurementsStack.Children.Add(new Label
        //    {
        //        Text = $"Dag: {m.Dag} | Dato: {m.Timestamp:dd-MM-yyyy} | Kl. {m.Timestamp:HH:mm} | {m.Type}: {m.Weight} g" +
        //               (m.TypiskDag ? " | Typisk dag" : "") +
        //               (!string.IsNullOrWhiteSpace(m.Kommentar) ? $" | Kommentar: {m.Kommentar}" : ""),
        //        FontSize = 14,
        //        TextColor = Colors.Black,
        //        Padding = new Thickness(5)
        //    });
        //}
    }
}




