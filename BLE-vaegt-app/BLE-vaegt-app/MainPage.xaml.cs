using System.Diagnostics.Metrics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using DataSkema_Library;

namespace BLE_vaegt_app
{
    public partial class MainPage : ContentPage
    {
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

        private async void ConnectAutomatically()
        {
            try
            {
                var savedId = Preferences.Get("BleDeviceId", null);
                if (string.IsNullOrEmpty(savedId))
                {
                    StatusLabel.Text = "Ingen gemt enhed. Scan først";
                    return;
                }

                StatusLabel.Text = "Forsøger at forbinde automatisk..";

                Guid deviceGuid = Guid.Parse(savedId);
                hm10Device = await adapter.ConnectToKnownDeviceAsync(deviceGuid);

                StatusLabel.Text = $"Forbundet til {hm10Device.Name}";
                await DataModtager();
            }

            catch (DeviceConnectionException)
            {
                StatusLabel.Text = "Kunne ikke forbinde automatisk. Tryk på knap";
            }
        }

        private async Task DataModtager()
        {
            var service = await hm10Device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            uartCharacteristic =
                await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));

            uartCharacteristic.ValueUpdated += (s, a) =>
            {
                var data = System.Text.Encoding.UTF8.GetString(a.Characteristic.Value);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    handler.HandleIncomingData(data, WeightLabel, LogData, FilLabel);
                });
            };

            await uartCharacteristic.StartUpdatesAsync();
        }

        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            StatusLabel.Text = "Scanner...";
            hm10Device = null;

            adapter.DeviceDiscovered += (s, a) =>
            {
                if (a.Device.Name != null && a.Device.Name.Contains("BLEVAEGT"))
                {
                    hm10Device = a.Device;
                }
            };

            await adapter.StartScanningForDevicesAsync();

            if (hm10Device == null)
            {
                StatusLabel.Text = "Ingen BLEVAEGT fundet";
                return;
            }

            await adapter.ConnectToDeviceAsync(hm10Device);
            StatusLabel.Text = $"Forbundet til {hm10Device.Name}";

            Preferences.Set("BleDeviceId",
                hm10Device.Id
                    .ToString()); //Gemmer værdien for HM10 modulet til at connecte automatisk til telefonen næste gang
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert //Skal oprettes i xaml. Note til mig selv. DisplayAlert findes allerede i 
        (
            "Slet log", // Title hedder den i xaml
            "Er du sikker på at du vil slette logfilen?", // Message
            "Ja", // Accept button
            "Nej" // Cancel button
        );

        if (confirm) //Returnere true
        {
            // User clicked "Ja"
            DeleteLog deleteLog = new DeleteLog();
            deleteLog.DeleteLogFile("vandladningskema.csv");
        }
        else
        {
            // User clicked "Nej"
            StatusLabel.Text = "Sletning annulleret"; //Skal oprettes i xaml
        }
    }
}

