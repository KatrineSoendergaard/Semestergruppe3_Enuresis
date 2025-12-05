using DataSkema_Library;
using System.Collections.ObjectModel;
using System.Text.Json; // Tilføjet for JSON-håndtering
using Microsoft.Maui.Storage; // VIGTIGT: Tilføjet for at finde appens filsti

namespace BLE_vaegt_app
{
    public static class GlobalData
    {
        // Din eksisterende ObservableCollection
        public static ObservableCollection<Measurement> Measurements { get; set; }
            = new ObservableCollection<Measurement>();

        public static string Navn { get; set; } = string.Empty;
        public static string Cpr { get; set; } = string.Empty;

        // filnavn for lagring
        private static readonly string fileName = "målingsskema.json";

        // Metode til at gemme data i en json streng
        public static async Task SaveMeasurements()
        {
            try
            {
                // Serialiser listen til en JSON-streng
                string jsonString = JsonSerializer.Serialize(Measurements);

                // Får den lokale filsti på telefonen
                string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Skriver strengen til filen
                await File.WriteAllTextAsync(fullPath, jsonString);
                
            }
            // Fejlhåndtering
            catch (Exception ex)
            {
                Console.WriteLine($"FEJL ved gemning af data: {ex.Message}");
            }
        }

        // Metode der indlæser gemt data når man åbner app
        // Er async så appen kan køre uden afbrydelser
        public static async Task LoadMeasurements()
        {
            try
            {
                // Dette er den sti som skal prøve at genoploades
                string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Tjek om filen eksisterer, før vi prøver at læse
                if (File.Exists(fullPath))
                {
                    // Læs JSON-strengen
                    string jsonString = await File.ReadAllTextAsync(fullPath);

                    // Deserialiser json strengen til C# objekter
                    var loadedList = JsonSerializer.Deserialize<ObservableCollection<Measurement>>(jsonString);

                    // Tjekker om deserialiseringen fungerede/returnede 
                    if (loadedList != null) // Returnere null hvis jsonstrengen var tom 
                    {
                        // Erstat den eksisterende liste med de indlæste data
                        Measurements = loadedList;
                    }
                }
            }
            catch (Exception ex)
            {
                //// Start med en tom liste, hvis indlæsning mislykkes
                //Measurements = new ObservableCollection<Measurement>();
            }
        }
    }
}

