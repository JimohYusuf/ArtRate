using ArtRate_Monitor.Services;
using ArtRate_Monitor.Views;
using ElmSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tizen.Network.Connection;
using Tizen.Sensor;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(HeartRate))]

namespace ArtRate_Monitor.Services
{

    public class HeartRate: IDisposable 
    {
        private HeartRateMonitor _sensor; 
        public HeartRate()
        {
            DataViewModel.intervalUpdated += DataViewModel_intervalUpdated;
        }

        private void DataViewModel_intervalUpdated()
        {
            Interval = DataViewModel._interval; 
        }

        ~HeartRate() 
        {
            Dispose(false);
            DataViewModel.intervalUpdated -= DataViewModel_intervalUpdated; 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private bool _disposed = false; 
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sensor.DataUpdated -= OnSensorDataUpdated;
                    _sensor.Dispose();
                }

                _sensor = null;
                _disposed = true;
            }
        }


        static string heartRateValDisp = "0"; 
        public static string HeartRateVal {
            get
            {
                return heartRateValDisp; 
            }
            set
            {
                heartRateValDisp = value; 
            }
        }

        static int interval = 1;
        public static int Interval
        {
            get { return interval; }
            set { interval = value; } 
        }

        public void Start()
        {

            if(_sensor != null)
            {
                try
                {
                    _sensor?.Start();
                    Toast.DisplayText("Heart Rate Monitoring Started", 1500);
                }
                catch (Exception e)
                {
                    Toast.DisplayText("Error Encountered. Please Try Again", 2000);
                    Logger.Info(e.Message);
                }
            }
            else
            {
                try 
                {

                    _sensor = new HeartRateMonitor();

                    _sensor.DataUpdated += OnSensorDataUpdated;

                    if (Interval > 0)
                    {
                        _sensor.Interval = (uint)Interval * 1000; 
                    }
                    else
                    {
                        Toast.DisplayText("Interval set to default (30 seconds)", 1500); 
                        _sensor.Interval = 30000;
                    }

                    Logger.Info("Interval: " + Interval.ToString()); 

                    _sensor.PausePolicy = SensorPausePolicy.None;
                }
                catch (NotSupportedException)
                {

                }
                catch (UnauthorizedAccessException)
                {

                }
                catch (Exception e)
                {
                    Logger.Fatal(e.Message);
                }

                try
                {
                    _sensor?.Start();
                    Toast.DisplayText("Heart Rate Monitoring Started", 1500);
                }
                catch (Exception e)
                {
                    Toast.DisplayText("Error Encountered. Please Try Again", 2000);
                    Logger.Info(e.Message);
                }
            }
            
        }
            

        public void Stop()
        {
            if(_sensor != null)
            {
                try
                {
                    _sensor?.Stop();
                    Toast.DisplayText("Heart Rate Monitoring Stopped", 1500);
                }
                catch (Exception e)
                {
                    Toast.DisplayText("Error Encountered. Please Try Again", 2000);
                    Logger.Info(e.Message);
                }
            }
            else
            {
                Toast.DisplayText("You have not started the service", 1500); 
            }
            
              
        }


        public static string heartRateValToServer { get; set; } = "0";
        public static event Action heartRateUpdt;
        private void OnSensorDataUpdated(object sender, HeartRateMonitorDataUpdatedEventArgs h) 
        {
            HeartRateVal = h.HeartRate.ToString() + " bpm";
            heartRateValToServer = h.HeartRate.ToString(); 
            heartRateUpdt();   
        }

    }
}
