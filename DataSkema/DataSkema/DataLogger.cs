using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSkema
{
    public static class DataLogger
    {
        // Her bruger vi bare den aktuelle mappe, i stedet for AppDataDirectory
        private static string filePath = Path.Combine(Environment.CurrentDirectory, "Vandladningsskema.csv");

        public static void AppendMeasurement(Measurement m)
        {
            string line = m.ToString();
            File.AppendAllText(filePath, line + Environment.NewLine); //AppendAllTekst tilføjer en ny linje til filen
        }

        public static string GetFilePath()
        {
            return filePath;
        }
    }
}

