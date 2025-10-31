namespace DataSkema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ALT der står her inde er kun for at teste at det virker
            // Her burde main hellere køre en klasse som har modtaget data fra arduino, når dette kan lade sig gøre
            Console.WriteLine(" Test af datalogger ");

            // Testmålinger
            Measurement m1 = new Measurement("Ble", 156.4);
            Measurement m2 = new Measurement("Urin", 80.6);
            Measurement m3 = new Measurement("Væske", 130.9);

            // Gemmer dem
            DataLogger.AppendMeasurement(m1);
            DataLogger.AppendMeasurement(m2);
            DataLogger.AppendMeasurement(m3);

            Console.WriteLine("Målinger gemt!");
            Console.WriteLine($"Fil gemt her: {DataLogger.GetFilePath()}");

            // Viser fil indholdet
            Console.WriteLine("\nIndhold i filen:");
            Console.WriteLine(File.ReadAllText(DataLogger.GetFilePath()));
        }
    }
}


