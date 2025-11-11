using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSkema_Library;

namespace BLE_vaegt_app
{
    public class MeasurementHandler
    {
        //Metode der modtager data fra Arduino
        public void HandleIncomingData(string data, Label weightLabel, Label logData, Label fileLabel) 
        {
            // Deler modtaget data op imellem " : "
            string[] dataSplitted = data.Split(':'); 

            // Sæt UI tekst
            weightLabel.Text = dataSplitted[0] + dataSplitted[1];

            // Konverter til double 
            double dataSplitted_w = double.Parse(dataSplitted[1]);

            // Opret måling og gem
            Measurement m1 = new Measurement(dataSplitted[0], dataSplitted_w);
            var logger = new DataLogger();
            logger.AppendMeasurement(m1);

            // Opdater labels
            logData.Text = File.ReadAllText(logger.GetFilePath());
            fileLabel.Text = logger.GetFilePath();
        }






    }
}
