using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;
using Woc.Helpers;
using System.Security.Policy;
using Com.OneSignal;

namespace Woc
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public string dummy1;
        public string dummy2;
        public string dummy3;
        public string dummy4;
        public string dummy6 = "Country";
        public string dummy5;
        public List<string> dumList = new List<string>();
        public MainPage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            this.BindingContext = this;
            this.IsBusy = false;
        }

        //Register button click
        private async void lblClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp(dummy1, dummy2, dummy3, dummy4, dummy5, dummy6));
        }

        // Login Button click
        private async void Button_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            CustomerClass customer = new CustomerClass();

            customer.email = emailID.Text;
            customer.user_name = emailID.Text;
            customer.password = pass.Text;
            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/LoginUser";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();

            Response response = JsonConvert.DeserializeObject<Response>(result);
            if (response.Status == 1 || response.Status == 2)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Enter Your Email and Password", "ok");
                
            }    
            else if (response.Status == 3)
            {
                
                OneSignal.Current.SendTag("user", "logged_in");
                Settings.AccessToken = response.SessionID.ToString();
                Settings.AccessName = response.Name;

                CustomerClass customer2 = new CustomerClass();
                customer2.customer_id = Convert.ToInt16(Settings.AccessToken);
                string url2 = "https://woccloudpublish.azurewebsites.net/api/Customer/checkPreference";
                HttpClient client2 = new HttpClient();
                string JsonData2 = JsonConvert.SerializeObject(customer2);
                StringContent content2 = new StringContent(JsonData2, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage2 = await client2.PostAsync(url2, content2);
                string result2 = await responseMessage2.Content.ReadAsStringAsync();
                Response response2 = JsonConvert.DeserializeObject<Response>(result2);

                this.IsBusy = false;

                if (response2.Status == 1)
                {
                    
                    await Navigation.PushAsync(new NavigationDrawer(Settings.AccessName));
                }
                else
                {
                   
                    await Navigation.PushAsync(new Preference(Settings.AccessToken, dumList,2));
                }
            }
            else
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Email or Password is incorrect", "ok");
            }
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
