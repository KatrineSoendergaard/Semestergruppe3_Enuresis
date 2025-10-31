using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using VæskeVandladningsSkema;

namespace VaeskeVandladningsSkema
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>();

            return builder.Build();

        }

        // Dette skal tilkobles til maui sådan at measurements sendes til klassen
        //Measurement m = new Measurement("Ble", 145.2);
        //DataLogger.AppendMeasurement(m);

    }
}
