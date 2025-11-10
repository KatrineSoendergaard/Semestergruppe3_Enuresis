using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;

namespace DataSkema_Library
{
    public class DeleteLog
    {
        public void DeleteLogFile(string filePath) // Nulstillingen af log efter endt behandling

        {
            try
            {

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"{filePath} nulstillet med succes.");
                }
                else
                {
                    Console.WriteLine("Filen eksistere ikke");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Der skete en fejl:" + ex.Message);
            }
            
        }


        // Kode til maui app. 
        // <Button x:Name="DeleteLogButton"
        //Text="Delete Log"
        //Clicked="DeleteLogButton_Clicked"
        //HorizontalOptions="Center"
        //VerticalOptions="Center" />
    }
}
