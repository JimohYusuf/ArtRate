using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using calmCom.Services;


namespace calmCom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var page = new CenterLayoutPage();
            Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false); 
        }
    }
}