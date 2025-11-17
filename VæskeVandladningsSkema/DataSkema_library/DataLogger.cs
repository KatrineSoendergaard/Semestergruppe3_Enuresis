using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using Microsoft.Maui.Storage;

namespace DataSkema_library
{
    public class DataLogger
    {
        private readonly string filePath;

        // Laver en fil hvis der ikke er en fil med samme navn i forvejen
        public DataLogger(string fileName = "vandladningskema.csv")
        {
            // Opretter en sti
            filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        }

        //Metode der indskriver en ny linje i filen
        public void AppendMeasurement(Measurement m)
        {
            //Konvertere til en string
            string line = m.ToString();
            //AppendAllTekst tilføjer en ny linje til filen
            File.AppendAllText(filePath, line + Environment.NewLine);
        }

        //Metode der returnere stien
        public string GetFilePath()
        {
            return filePath;
        }
    }
}
