namespace BLE_vaegt_app.Pages;

public partial class LogSkema : ContentPage
{

    public LogSkema()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        // Dette sikrer, at standardopførsel(
        base.OnAppearing();

        // Tøm tidligere viste målinger.Dette forhindrer, at gamle data vises flere gange, hvis siden genåbnes
        MeasurementsStack.Children.Clear();

        // Gennemgå alle gemte målinger i den globale liste
        foreach (var data in GlobalData.SkemaData)
        {
            // Opret en ny Label for hver måling så den ik 
            // Teksten i Label sættes til selve dataen
            // FontSize og TextColor bestemmer udseendet
            MeasurementsStack.Children.Add(new Label
            {
                Text = data,                //målingstekst
                FontSize = 14,              //skriftstørrelse
                TextColor = Colors.Black    //tekstfarve
            });
        }
    }

}