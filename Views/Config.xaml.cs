using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtRate_Monitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Config : ContentPage
    {
        public Config()
        {
            InitializeComponent();

            BindingContext = ViewModelSingleton.MainViewModel; 
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var page = new CenterLayoutPage();
            _ = Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false); 

        }
    }
}