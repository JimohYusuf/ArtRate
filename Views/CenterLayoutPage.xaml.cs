using System;

using Tizen.Wearable.CircularUI.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ArtRate_Monitor.Services;


namespace ArtRate_Monitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CenterLayoutPage : ContentPage
    {
        public CenterLayoutPage()
        {
            InitializeComponent();

            BindingContext = ViewModelSingleton.MainViewModel;   

        }

    }   
}
