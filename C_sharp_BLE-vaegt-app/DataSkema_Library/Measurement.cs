using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSkema_Library
{
    public class Measurement
        {
            // Typen kan være BLE, URIN el. VÆSKE
            public string Type { get; set; }
            // Hvor meget der er blevet målt i gram
            public double Weight { get; set; }
            // DateTime giver den specifikke tidspunkt målingen er taget 
            public DateTime Timestamp { get; set; } 

        public Measurement(string type, double weight)
        {
            Type = type;
            Weight = weight;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            //Dette er sådan det bliver skrevet ind i filen
            return $"{Timestamp:dd-MM-yyyy HH:mm}, {Type}, {Weight} g"; 
        }
    }
}

