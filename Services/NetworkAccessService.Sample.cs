using ArtRate_Monitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tizen.Messaging.Push;
using Tizen.Multimedia;
using Tizen.Network.Connection;
using Tizen.Network.IoTConnectivity;
using Tizen.Pims.Contacts.ContactsViews;
using Tizen.Uix.InputMethod;
using Tizen.Uix.Stt;
using Tizen.Wearable.CircularUI.Forms;

namespace ArtRate_Monitor.Services
{
    public partial class NetworkAccessService
    {

        static string url = string.Empty; 
        public static string URL
        {
            get { return url; }
            set
            {
                url = "http://" + value;
                Tizen.Applications.Preference.Set("ip_address", value); 
            }
        }

        static bool sendStatus = false;
        public static bool SendStatus
        { 
            get { return sendStatus; }
            set { sendStatus = value; }
        }

        public static void setURL()
        {
            URL = DataViewModel.url; 
            Logger.Info("On Changed: " + URL); 
        }


        static HttpResponseMessage serverResponse;
        public static HttpResponseMessage Response
        {
            get { return serverResponse; }
            set { serverResponse = value; } 
        }
        public static async Task SendWebRequestSampleAsync() 
        {
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

                var data = new HrData() { heartRateVal = HeartRate.heartRateValToServer};
                var content = JsonConvert.SerializeObject(data); 

                var heartRate = data.heartRateVal; 


                client = new HttpClient(handler);
#pragma warning disable CA2000 
                Response = await client.PostAsync(new Uri(URL), new StringContent(content));
#pragma warning restore CA2000 

                Logger.Info("URL: " + URL);

                if (Response.IsSuccessStatusCode)
                    SendStatus = true;
                else
                    SendStatus = false; 

                Logger.Info($"response: {Response}"); 
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
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
