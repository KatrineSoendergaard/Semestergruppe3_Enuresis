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
        // Metode der nulstiller log efter endt behandling
        public void DeleteLogFile(string filePath) 

        {
            try
            {
                //Tjekker om filen findes
                if (File.Exists(filePath)) 
                {
                    //Sletter filen og alt data
                    File.Delete(filePath); 
                    Console.WriteLine($"{filePath} nulstillet med succes.");
                }
                else
                {
                    // hvis den ikke kan finde den skriver den dette ud i console
                    Console.WriteLine("Filen eksistere ikke"); 
                }
            }
            //Dette er kun hvis der sker en fejl
            catch (Exception ex) 
            {
                // Udskriver hvilken fejl der er sket
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
