using ArtRate_Monitor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tizen.Network.Connection;
using Tizen.System;
using Xamarin.Forms;

namespace ArtRate_Monitor
{   
    public static class ViewModelSingleton
    {
        static DataViewModel dataViewModel = new DataViewModel();
        public static DataViewModel MainViewModel
        {
            get
            {
                return dataViewModel; 
            }
        }
    } 

    public class DataViewModel: INotifyPropertyChanged 
    {
        public Command StartHRSensorService { get; set; }
        public Command StopHRSensorService { get; set; } 

        public static readonly HeartRate hrService = new HeartRate();
        public static readonly NetworkAccessService networkService = new NetworkAccessService();

        public static event Action addressUpdatedFromExt;

        public static event Action intervalUpdated; 

        ConnectionItem connection = ConnectionManager.CurrentConnection;

        public DataViewModel()
        {            
            StartHRSensorService = new Command(startHRSensorService);
            StopHRSensorService = new Command(stopHRSensorService);

            if (Tizen.Applications.Preference.Contains("ip_address"))
            {
               Address = Tizen.Applications.Preference.Get<string>("ip_address");
            }
        }

      

        string heartRateVal = HeartRate.HeartRateVal; 
        public string HeartRateVal
        {
            get
            {
                return heartRateVal;
            }
            set
            {
                heartRateVal = value;
                OnPropertyChanged(nameof(HeartRateVal)); 
            }
        }


        string networkState = NetworkAccessService.ConnState;  
        public string NetworkState
        {
            get
            {
                return networkState;
            }
            set
            {
                networkState = value;
                OnPropertyChanged(nameof(NetworkState)); 
            }
        }


        public static string url = string.Empty; 
        string address = string.Empty;
        public string  Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                url = address;
                OnPropertyChanged(nameof(Address));
                addressUpdatedFromExt();   
            }
        }

        public static int _interval = 30;
        int interval = 30; 
        public int Interval
        {
            get { return interval; }
            set 
            { 
                interval = (int)value; 
                _interval = interval;
                OnPropertyChanged(nameof(Interval));
                intervalUpdated();
            } 
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        } 


        async Task heartRateChanged() 
        {
            HeartRateVal = HeartRate.HeartRateVal;

            if (!(connection.Type == ConnectionType.Disconnected)) 
                await postHeartRate(); 
        }


        void networkStateChanged()
        {
            NetworkState = NetworkAccessService.ConnState;  
        }


        async Task postHeartRate() 
        {
            await NetworkAccessService.SendWebRequestSampleAsync();  
        }


        void startHRSensorService()
        {
            hrService.Start(); 
            HeartRate.heartRateUpdt += async () => await heartRateChanged();
            NetworkAccessService.connStateUpdt += networkStateChanged;    
        }


        void stopHRSensorService() 
        { 
            HeartRate.heartRateUpdt -= async () => await heartRateChanged();
            NetworkAccessService.connStateUpdt -= networkStateChanged;
            hrService.Stop();  
        } 


    }
}
