using ArtRate_Monitor.Models;
using ArtRate_Monitor.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tizen.Applications;
using Tizen.Network.Connection;
using Tizen.Pims.Contacts.ContactsViews;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtRate_Monitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Connect : ContentPage
    {
        static bool sendStatus = false;
        public static bool SendStatus
        {
            get { return sendStatus; }
            set { sendStatus = value; }
        }

        static HttpResponseMessage serverResponse;
        public static HttpResponseMessage Response
        {
            get { return serverResponse; }
            set { serverResponse = value; }
        }

        public Connect()
        {
            InitializeComponent();

            BindingContext = ViewModelSingleton.MainViewModel;

        }

        private async void Button_Clicked(object sender, EventArgs h)
        {

            var url = NetworkAccessService.URL;
 
            ConnectionItem connection = ConnectionManager.CurrentConnection; 

            if (connection.Type == ConnectionType.Disconnected)
            {
                Toast.DisplayText("No Internet Connection", 1000); 
                return;
            }

            HttpClientHandler handler = null;
            HttpClient client = null;

            try
            {
                handler = new HttpClientHandler();

                // When a watch has a Bluetooth connection to a phone, a proxy to access the internet through the phone is served by default
                if (connection.Type == ConnectionType.Ethernet)
                {
                    var proxy = ConnectionManager.GetProxy(AddressFamily.IPv4);
                    handler.Proxy = new WebProxy(proxy, true);
                } 

                var data = new HrData() { heartRateVal = "0" }; 
                var content = JsonConvert.SerializeObject(data);
                var SendStatus = false; 

                client = new HttpClient(handler);
#pragma warning disable CA2000 
                var Response = await client.PostAsync(new Uri(url), new StringContent(content)); 
#pragma warning restore CA2000

                Logger.Info($"response: {Response}");

                if (Response.IsSuccessStatusCode)
                    SendStatus = true;
                else
                    SendStatus = false;

                if (SendStatus == true)
                {
                    var page = new Config(); 
                    _ = Navigation.PushAsync(page);
                    NavigationPage.SetHasNavigationBar(page, false);
                }
                else
                {
                    Toast.DisplayText("Cannot Connect", 1500); 
                } 
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Toast.DisplayText("Cannot Connect", 1500); 
                Logger.Info(e.Message);
            }
            finally
            {
                client?.Dispose();
                handler?.Dispose();
            }

           



        }
    }
}