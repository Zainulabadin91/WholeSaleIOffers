using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Woc.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

        
namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
       
       
        public HomePage()
        {
            InitializeComponent();
 
        }

        //Available offers button click
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
             Navigation.PushAsync(new PushNotification());
        }

        //About us button click
        private void Button_Clicked_1(object sender, EventArgs e)
        {

            Navigation.PushAsync(new AboutUs());
        }

        //Contact us button click
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactUs());
        }
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        //Cart button click
        private void Button_Clicked_4(object sender, EventArgs e)
        {

        }

        //Back button pressing for exiting app
        protected override bool OnBackButtonPressed()
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            var result = await DisplayAlert("", "Would you like to exit from application?", "Yes", "No");
            if (result)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    System.Environment.Exit(0);
                }
                else if (Device.OS == TargetPlatform.iOS)
                {
                    System.Environment.Exit(0);
                }
            }
        });
        return true;
    }
    }
}