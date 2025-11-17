using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLE_vaegt_app
{
    public static class GlobalData
    {
        public static List<string> SkemaData { get; set; } = new List<string>();

        // Gem nuværende bruger
        public static string Navn { get; set; } = string.Empty;
        public static string Cpr { get; set; } = string.Empty;
    }
}
