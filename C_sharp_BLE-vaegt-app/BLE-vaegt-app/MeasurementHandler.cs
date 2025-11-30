using System;
using System.Threading.Tasks;
using DataSkema_Library;
using Microsoft.Maui.Controls;

namespace BLE_vaegt_app
{
    public class MeasurementHandler
    {
        // async da den gør brug af await og task
        // Metode der styre hvad der sker med dataen fra vægten
        public async void HandleIncomingData(string data)
        {
            try
            {
                // Modtager et array af strings. Den splitter dataen op ved ":"
                // Dette gør vi har to parts = parts[0] og parts[1]
                string[] parts = data.Split(':');

                // Søger for at vi skal modtage noget der splittes op i mindst 2 parts
                // Ellers returnere den
                if (parts.Length < 2)
                    return;

                // Tjekker om den første del af parts er af typen string
                string type = parts[0];

                // Prøver at konvertere parts[1] til en double.
                // Hvis det sker gemmes den nye double værdi i weight
                if (!double.TryParse(parts[1], out double weight))
                    return;

                // Opretter Measurement objekt
                var measurement = new Measurement(type, weight)
                {
                    Dag = "-",
                    TypiskDag = false,
                    Kommentar = "",
                    Timestamp = DateTime.Now
                };

                // Gemmer i den liste i globaldata
                GlobalData.Measurements.Add(measurement);

               // Starter gemningen af målingerne i en braggrundstråd, sådan at UI'en kan køre uden der sker afbrydelser. 
                Task.Run(async () => await GlobalData.SaveMeasurements());

            }
            // Fejlhåndtering
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved databehandling/visning: " + ex.Message);
            }
        }
    }
}
