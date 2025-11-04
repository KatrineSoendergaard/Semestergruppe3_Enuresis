using System.Diagnostics.Metrics;
using Microsoft.Maui.Storage;


namespace DataSkema_Library
{
    public class DataLogger
    {
        private readonly string filePath;

        public DataLogger(string fileName = "vandladningskema.csv")
        {
            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        }

        // Her bruger vi bare den aktuelle mappe, i stedet for AppDataDirectory
        // = Path.Combine(Environment.AppDataDirectory, "Vandladningsskema.csv");

        public void AppendMeasurement(Measurement m)
        {
            string line = m.ToString();
            File.AppendAllText(filePath, line + Environment.NewLine); //AppendAllTekst tilføjer en ny linje til filen
        }

        public string GetFilePath()
        {
            return filePath;
        }
    }
}
