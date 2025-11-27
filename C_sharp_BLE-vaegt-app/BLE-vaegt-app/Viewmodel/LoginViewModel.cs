using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BLE_vaegt_app.viewmodel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        //Metode for navn
        private string navn;
        public string Navn
        {
            get => navn;
            set
            {
                if (navn != value)
                {
                    // Kun bogstaver i bogstavsfeltet
                    navn = Regex.Replace(value ?? "", @"[^a-zA-ZæøåÆØÅ\s]", "");
                    OnPropertyChanged(nameof(Navn));
                }
            }
        }

        //Metode på CPR
        private string cpr;
        public string Cpr
        {
            get => cpr;
            set //Alternativ til events(xaml)
            {
                if (cpr != value)
                {
                    // Kun tal + max 10 cifre
                    string newValue = Regex.Replace(value ?? "", @"[^0-9]", "");
                    if (newValue.Length > 10)
                        newValue = newValue.Substring(0, 10);
                    cpr = newValue;
                    OnPropertyChanged(nameof(Cpr));
                }
            }
        }

        //Metode der registrere knaptryk
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        // Metode der reseter navn og cpr. Arbejder sammen med onAppearing i xaml.cs
        public void OnAppearing()
        {
            Navn = string.Empty;
            Cpr = string.Empty;
        }

        //Metode der validerer CPR og Navn
        private async void OnLogin()
        {
            // Validering
            if (string.IsNullOrWhiteSpace(Navn))
            {
                await Shell.Current.DisplayAlert("Fejl", "Navn skal udfyldes.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Cpr) || Cpr.Length != 10)
            {
                await Shell.Current.DisplayAlert("Fejl", "CPR skal være præcis 10 cifre.", "OK");
                Cpr = string.Empty;
                return;
            }
            // Gem globalt
            GlobalData.Navn = Navn;
            GlobalData.Cpr = Cpr;
            // Navigation
            await Shell.Current.GoToAsync($"WelcomePage?userName={Navn}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

