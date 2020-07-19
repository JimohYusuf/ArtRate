using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Tizen.Network.Connection;

namespace ArtRate_Monitor.Services
{
    public partial class NetworkAccessService
    {

        public NetworkAccessService()
        {
            ConnectionManager.ConnectionTypeChanged += OnConnectionTypeChanged;

            DataViewModel.addressUpdatedFromExt += setURL;
        }

        ~NetworkAccessService()
        {
            ConnectionManager.ConnectionTypeChanged -= OnConnectionTypeChanged;

            DataViewModel.addressUpdatedFromExt -= setURL;
        }


        static string connState = "disconnected"; 
        public static string ConnState
        {
            get{ return connState;}
            set 
            {
                connState = value;
            } 
        }


        public static event Action connStateUpdt; 
        private void OnConnectionTypeChanged(object sender, ConnectionTypeEventArgs e)
        {
            // TODO: Insert code to monitor the network state
            if (e.ConnectionType == ConnectionType.Disconnected)
            {
                ConnState = "disconnected";
            }
            else if (e.ConnectionType == ConnectionType.Ethernet)
            {
                ConnState = "ethernet active"; 
            }
            else if (e.ConnectionType == ConnectionType.Cellular)
            {
                ConnState = "cellular active";
            }
            else if (e.ConnectionType == ConnectionType.WiFi)
            {
                ConnState = "Wi-Fi active"; 
            }

            connStateUpdt(); 
        }
    }
}
