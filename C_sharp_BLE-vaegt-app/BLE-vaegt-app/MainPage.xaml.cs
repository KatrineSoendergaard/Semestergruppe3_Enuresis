using System.Diagnostics.Metrics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using DataSkema_Library;
using Microsoft.Maui.Controls.Hosting;

namespace BLE_vaegt_app
{
    public partial class MainPage : ContentPage
    {
        // Fields
        IAdapter adapter;
        IBluetoothLE ble;
        IDevice hm10Device;
        ICharacteristic uartCharacteristic;
        MeasurementHandler handler;

        public MainPage()
        {
            InitializeComponent();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            handler = new MeasurementHandler();

            ConnectAutomatically();
        }

        // Metode der connecter automatisk til sidst kendte bluetooth modul
        //Den er async så andre metoder stadigvæk kan udføre andre ting imens
        private async void ConnectAutomatically()
        {
            try
            {
                // Tjekker om den kan skabe forbindelse til sidst kendte bluetooth modul
                var savedId = Preferences.Get("BleDeviceId", null);

                // Hvis der ikke er gemt et bluetooth modul
                if (string.IsNullOrEmpty(savedId))
                {
                    StatusLabel.Text = "Ingen gemt enhed. Scan først";
                    // Springer ud af metoden hvis der ingen endhed er gemt
                    return;
                }

                // Dette kører hvis der er gemt et bluetooth modul
                StatusLabel.Text = "Forsøger at forbinde automatisk..";

                // Konverterer savedId(string) til en GUID(globally unique identifier)
                Guid deviceGuid = Guid.Parse(savedId);
                // Venter på at connection er fuldført uden at stoppe Thread 
                hm10Device = await adapter.ConnectToKnownDeviceAsync(deviceGuid);

                // Udskriver i UI at den er forbundet til vægten/bluetooth modulet
                StatusLabel.Text = $"Forbundet til {hm10Device.Name}";
                // Venter på at den modtager data så metoden Datamodtager kan begynde
                await DataModtager();
            }

            // Laver en catch sådan at vores app ikke crasher
            catch (DeviceConnectionException)
            {
                StatusLabel.Text = "Kunne ikke forbinde automatisk. Tryk på knap";
            }
        }

        // Metode der modtager data fra bluetooth modulet
        private async Task DataModtager()
        {
            try
            {
                // Konverterer dataen fra bytes til en string
                var service = await hm10Device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
                uartCharacteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));

                uartCharacteristic.ValueUpdated += (s, a) =>
                {
                    var data = System.Text.Encoding.UTF8.GetString(a.Characteristic.Value);

                    // Thread der gør brug af metode fra klassen MeasurementHandler
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        handler.HandleIncomingData(data);
                    });
                };

                // Fortæller bluetooth deviced at den skal sende data
                await uartCharacteristic.StartUpdatesAsync();
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Fejl i DataModtager: {ex.Message}";
                Console.WriteLine($"DataModtager fejl: {ex}");
            }
        }

        // Metode der anmoder om Bluetooth tilladelser
        private async Task RequestBluetoothPermissions()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<BluetoothPermission>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<BluetoothPermission>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tilladelsesproblem: {ex.Message}");
            }
        }

        // Metode der connecter til Bluetooth hvis der trykkes på Scan efter enhed knap
        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            // Anmod om tilladelser
            await RequestBluetoothPermissions();

            StatusLabel.Text = "Scanner...";
            // Indstiller hm10 til null sådan at der ikke er noget gemt på den
            hm10Device = null;

            // Lampda metode der kører hver gang den finder et bluetooth modul
            adapter.DeviceDiscovered += (s, a) =>
            {
                // Tjekker om bluetooth modulet har et navn og om dette navn er BLEVAEGT 
                if (a.Device.Name != null && a.Device.Name.Contains("BLEVAEGT"))
                {
                    // Gemmer modulet hvis det matcher vores kriterier
                    hm10Device = a.Device;
                    Console.WriteLine($"Fundet enhed: {a.Device.Name}");
                }
            };


            // Denne kører hele tiden og gør at DeviceDiscovered starter hver gang der findes et modul. 
            await adapter.StartScanningForDevicesAsync();

            // Stop scanning 
            await adapter.StopScanningForDevicesAsync();

            // Dette tjekker om der er fundet det rigtige bluetooth modul
            if (hm10Device == null)
            {
                StatusLabel.Text = "Ingen BLEVAEGT fundet";
                // Stopper metoden hvis der ikke er fundet BLEVAEGT
                return;
            }

            try
            {
                // Prøver at connecte til det fundede bluetooth modul
                await adapter.ConnectToDeviceAsync(hm10Device);
                StatusLabel.Text = $"Forbundet til {hm10Device.Name}";

                // Start datamodtagelse
                await DataModtager();
                //Gemmer værdien for HM10 modulet til at connecte automatisk til telefonen næste gang
                Preferences.Set("BleDeviceId", hm10Device.Id.ToString());
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Forbindelsesfejl: {ex.Message}";
                Console.WriteLine($"Forbindelsesfejl: {ex}");
            }
        }
    }

    // BLUETOOTH PERMISSION KLASSE VIGTIGT MÅ IKKE SLETTES
    // Denne her klasse er den som arbejder sammen med Androidmanifest der gør at vi har tilladelserne iorden til bluetooth. 
    public class BluetoothPermission : Permissions.BasePlatformPermission
    {
    }
}





// Alt der står i denne klasse skal ikke nødvendigvis have UI.
// Evt. en popup til connect til vægt (så de ikke tager måling før den er connected

//ALT KODEN HERUNDER VIRKER !!!!!!!!!!!!!!

//namespace BLE_vaegt_app
//{
//    public partial class MainPage : ContentPage
//    {
//        // Fields
//        IAdapter adapter;
//        IBluetoothLE ble;
//        IDevice hm10Device;
//        ICharacteristic uartCharacteristic;
//        MeasurementHandler handler;

//        public MainPage()
//        {
//            // Constructors 
//            InitializeComponent();
//            ble = CrossBluetoothLE.Current;
//            adapter = CrossBluetoothLE.Current.Adapter;
//            handler = new MeasurementHandler();

//            //handler.HandleIncomingData("ts:20.8", WeightLabel, LogData, FilLabel);  DETTE ER KODEN TIL HVIS VI SKAL TEST.

//            ConnectAutomatically();
//        }

//        // Metode der connecter automatisk til sidst kendte bluetooth modul.
//        // Den er async så programmet stadigvæk kan udføre andre ting imens
//        private async void ConnectAutomatically()
//        {
//            try
//            {
//                // Tjekker om den kan skabe forbindelse til sidst kendte bluetooth modul
//                var savedId = Preferences.Get("BleDeviceId", null);

//                // Hvis der ikke er gemt et bluetooth modul
//                if (string.IsNullOrEmpty(savedId))
//                {
//                    StatusLabel.Text = "Ingen gemt enhed. Scan først";
//                    // Springer ud af metoden hvis der ingen endhed er gemt
//                    return;
//                }

//                // Dette kører hvis der er gemt et bluetooth modul
//                StatusLabel.Text = "Forsøger at forbinde automatisk..";

//                // Konverterer savedId(string) til en GUID(globally unique identifier)
//                Guid deviceGuid = Guid.Parse(savedId);
//                // Venter på at connection er fuldført uden at stoppe Thread 
//                hm10Device = await adapter.ConnectToKnownDeviceAsync(deviceGuid);

//                // Udskriver i UI at den er forbundet til vægten/bluetooth modulet
//                StatusLabel.Text = $"Forbundet til {hm10Device.Name}";
//                // Venter på at den modtager data så metoden Datamodtager kan begynde
//                await DataModtager();
//            }

//            // Laver en catch sådan at vores app ikke crasher
//            catch (DeviceConnectionException)
//            {
//                StatusLabel.Text = "Kunne ikke forbinde automatisk. Tryk på knap";
//            }
//        }

//        // Metode der modtager data fra bluetooth modulet
//        private async Task DataModtager()
//        {
//            var service = await hm10Device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
//            uartCharacteristic =
//                await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));

//            uartCharacteristic.ValueUpdated += (s, a) =>
//            {
//                // Konverterer dataen fra bytes til en string
//                var data = System.Text.Encoding.UTF8.GetString(a.Characteristic.Value);

//                // Thread der gør brug af metode fra klassen MeasurementHandler
//                MainThread.BeginInvokeOnMainThread(() =>
//                {
//                    handler.HandleIncomingData(data/*, WeightLabel, LogData, FilLabel*/);
//                });
//            };

//            // Fortæller bluetooth deviced at den skal sende data
//            await uartCharacteristic.StartUpdatesAsync();
//        }


//        //Metode der connecter til Bluetooth hvis der trykkes på Scan efter enhed knap
//        private async void ScanButton_Clicked(object sender, EventArgs e)
//        {
//            StatusLabel.Text = "Scanner...";
//            // Indstiller hm10 til null sådan at der ikke er noget gemt på den
//            hm10Device = null;

//            // Lampda metode der kører hver gang den finder et bluetooth modul
//            adapter.DeviceDiscovered += (s, a) =>
//            {
//                // Tjekker om bluetooth modulet har et navn og om dette navn er BLEVAEGT 
//                if (a.Device.Name != null && a.Device.Name.Contains("BLEVAEGT"))
//                {
//                    // Gemmer modulet hvis det matcher vores kriterier
//                    hm10Device = a.Device;
//                }
//            };

//            // Denne kører hele tiden og gør at DeviceDiscovered starter hver gang der findes et modul. 
//            await adapter.StartScanningForDevicesAsync();

//            // Dette tjekker om der er fundet det rigtige bluetooth modul
//            if (hm10Device == null)
//            {
//                StatusLabel.Text = "Ingen BLEVAEGT fundet";

//                // Stopper metoden hvis der ikke er fundet BLEVAEGT
//                return;
//            }

//            // Prøver at connecte til det fundede bluetooth modul
//            await adapter.ConnectToDeviceAsync(hm10Device);
//            StatusLabel.Text = $"Forbundet til {hm10Device.Name}";

//            //Gemmer værdien for HM10 modulet til at connecte automatisk til telefonen næste gang
//            Preferences.Set("BleDeviceId", hm10Device.Id.ToString());

//        }
//    }
//}




