using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
//using ObjCBindings;

namespace BLE_vaegt_app.viewmodel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Private field til at gemme navn internt
        private string navn;

        // Property som Xaml kan binde til
        public string Navn
        {
            // Returnerer det gemte navn
            get => navn;

            // Køres når navn bliver ændret
            set
            { 
                // Tjekker om værdien er blevet ændret
                if (navn != value)
                {
                    // Kun bogstaver + danske tegn og mellemrum i bogstavsfeltet
                    navn = Regex.Replace(value ?? "", @"[^a-zA-ZæøåÆØÅ\s]", "");
                    
                    // Fortæller UI at navn er ændret
                    OnPropertyChanged(nameof(Navn));
                }
            }
        }

        //Private field til at gemme CPR internt
        private string cpr;

        // Property som Xaml kan binde til
        public string Cpr
        {
            // Returnerer det gemte CPR
            get => cpr;
            set //Alternativ til events(xaml)
            {
                // Tjekker om værdien
                if (cpr != value)
                {
                    // Fjerner alt andet end tal
                    string newValue = Regex.Replace(value ?? "", @"[^0-9]", "");

                    // Max 10 cifre
                    if (newValue.Length > 10)
                        newValue = newValue.Substring(0, 10);
                    
                    // Gemmer den nye værdi
                    cpr = newValue;

                    // Fortæller UI at navn er ændret
                    OnPropertyChanged(nameof(Cpr));
                }
            }
        }

        //Metode der registrere knaptryk
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            // Knytter LoginCommand til OnLogin-metode. Når der trykkes på login-knappen
            LoginCommand = new Command(OnLogin);
        }

        // Metode der reseter navn og cpr. Arbejder sammen med onAppearing i xaml.cs
        public void OnAppearing()
        {
            // Tømmer navn-felt
            Navn = string.Empty;

            // Tømmer CPR-feltet
            Cpr = string.Empty;
        }

        //Metode der validerer CPR og Navn
        private async void OnLogin()
        {
            // Valider at navn er udfyldt
            if (string.IsNullOrWhiteSpace(Navn))
            {
                await Shell.Current.DisplayAlert("Fejl", "Navn skal udfyldes.", "OK");
                return;
            }
            // Valider at CPR er udfyldt og har præcis 10 cifre
            if (string.IsNullOrWhiteSpace(Cpr) || Cpr.Length != 10)
            {
                await Shell.Current.DisplayAlert("Fejl", "CPR skal være præcis 10 cifre.", "OK");
                
                // Fjerner det indtastede i CPR-feltet
                Cpr = string.Empty; 
                return;
            }

            // Gem navn og CPR så de kan bruges på andre sider i appen
            GlobalData.Navn = Navn;
            GlobalData.Cpr = Cpr;

            // Naviger til WelcomePage og send brugernavnet med som parameter
            await Shell.Current.GoToAsync($"WelcomePage?userName={Navn}");
        }

        // Dette event bruges til at notificere UI'en når properties ændres
        public event PropertyChangedEventHandler PropertyChanged;

        // Hjælpemetode som udsender PropertyChanged-eventet
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

