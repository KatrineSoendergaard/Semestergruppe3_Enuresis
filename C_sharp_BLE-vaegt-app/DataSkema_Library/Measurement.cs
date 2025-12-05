using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSkema_Library
{
    public class Measurement
        {
        /// Dag fra bruger (Dag 1, Dag 2 osv.)
        public string Dag { get; set; }

        // Typen kan være BLE, URIN el. VÆSKE
        public string Type { get; set; }
        // Hvor meget der er blevet målt i gram
        public double Weight { get; set; }
        // DateTime giver den specifikke tidspunkt målingen er taget 
        public DateTime Timestamp { get; set; }

        public bool TypiskDag { get; set; }        // Til krydset af "typisk dag"
        public string Kommentar { get; set; }      // Eventuel kommentar


        public Measurement(string type, double weight)
        {
            Type = type;
            Weight = weight;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            //Dette er sådan det bliver skrevet ind i filen
            return $" | {Timestamp:HH:mm} | {Type}: {Weight} g";
        }
    }
}
//{ Dag}

