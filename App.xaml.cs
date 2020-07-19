using System;
using System.Runtime.InteropServices;
using ArtRate_Monitor.Services;
using ArtRate_Monitor.Views;
using Microsoft.VisualBasic;
using Tizen.Applications;
using Tizen.Applications.Exceptions;
using Tizen.System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ArtRate_Monitor
{
    public partial class App
    {
        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_request_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerRequestLock(int type, int timeout_ms);

        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_release_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerReleaseLock(int type); 

        enum Power_Type { CPU = 0, DISPLAY = 1, DISPLAY_DIM = 2 };

        public App()
        {
            InitializeComponent();

            //sDisplay.StateChanged += Display_StateChanged;

            MainPage = new NavigationPage(new Connect());
        }

        //private void Display_StateChanged(object sender, DisplayStateChangedEventArgs e)
        //{
        //    if (e.State == DisplayState.Normal)
        //    {
        //        AppControl appcontrol = new AppControl()
        //        {
        //            ApplicationId = Tizen.Applications.Application.Current.ApplicationInfo.ApplicationId 
        //        };
        //        AppControl.SendLaunchRequest(appcontrol);
        //    }
        //}

        ~App()
        {
            //Release lock when app is closed 
            _ = DevicePowerReleaseLock((int)Power_Type.CPU);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
             
            // The app has to request the permission to access sensors
            RequestPermissionSensorAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume() 
        {
            //Allow app to run in background 
            _ = DevicePowerRequestLock((int)Power_Type.CPU, 0); 
          //  _ = DevicePowerRequestLock((int)Power_Type.DISPLAY, 0); 
        } 

        /// <summary>
        /// The app has to request the permission to access sensors.
        /// </summary>
        private async void RequestPermissionSensorAsync()
        {
            var response = await PrivacyPermissionService.RequestAsync(PrivacyPrivilege.HealthInfo);
            if (response == PrivacyPermissionStatus.Granted)
            {
                // TODO: The permission was granted
            }
            else
            {
                Toast.DisplayText("App Cannot Work Without Required Permission", 2500);
            }
        }
    }
}
