using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSkema_Library;
using BLE_vaegt_app.Pages;

namespace BLE_vaegt_app
{
   public class MeasurementHandler
        {
            public void HandleIncomingData(string data, Label weightLabel, Label logData, Label fileLabel) 
            {
                try
                {
                    string[] dataSplitted = data.Split(':');

                    if (dataSplitted.Length < 2)
                        return;

                    weightLabel.Text = dataSplitted[0] + dataSplitted[1];
                    
                    // Konverter til double
                    double dataSplitted_w = double.Parse(dataSplitted[1]);

                    // Opret måling og gem
                    Measurement m1 = new Measurement(dataSplitted[0], dataSplitted_w);
                    var logger = new DataLogger();
                    logger.AppendMeasurement(m1);

                    logData.Text = File.ReadAllText(logger.GetFilePath());
                    fileLabel.Text = logger.GetFilePath();

                    // AUTO gem i global
                    string tid = DateTime.Now.ToString("HH:mm");
                    string type = dataSplitted[0]; // BLE, URIN, VÆSKE etc.
                    string dato = DateTime.Now.ToString("dd-MM-yyyy");

                    // Format som i manuel
                    string formattedData = $"Dato: {dato}, Tid: {tid}, Type: {type}, Vægt: {dataSplitted_w}g";

                    // Putter direkte i globaldata. 
                    GlobalData.SkemaData.Add(formattedData);
                }
                catch (Exception ex)
                {
                    weightLabel.Text = "Fejl ved databehandling";
                }
            }
        }
    }





















// ALT DEN GAMLE KODE FINDES HERUNDER !!!!!!!!!


//Metode der modtager data fra Arduino
//    public void HandleIncomingData(string data, Label weightLabel, Label logData, Label fileLabel) 
//    {
//        // Deler modtaget data op imellem " : "
//        string[] dataSplitted = data.Split(':'); 

//        // Sæt UI tekst
//        weightLabel.Text = dataSplitted[0] + dataSplitted[1];

//        // Konverter til double 
//        double dataSplitted_w = double.Parse(dataSplitted[1]);

//        // Opret måling og gem
//        Measurement m1 = new Measurement(dataSplitted[0], dataSplitted_w);
//        var logger = new DataLogger();
//        logger.AppendMeasurement(m1);

//        // Opdater labels
//        logData.Text = File.ReadAllText(logger.GetFilePath());
//        fileLabel.Text = logger.GetFilePath();


//}
