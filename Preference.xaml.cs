using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Woc.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Preference : ContentPage
    {
       public static int cID;
       public static int removeId;
        public Preference(string sId, List<string> list, int id)
        {
            InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            cID = Convert.ToInt16(sId);
            removeId = id;
            if (id == 1)
            {
                SetValue(NavigationPage.HasNavigationBarProperty, true);
                skip.IsVisible = false;
                line1.Text = "My Preferences";
                line2.IsVisible = false;
                btn.Text = "Update";

                if (list.Contains("Knife"))
                {
                    check1.IsChecked = true;
                }
                if (list.Contains("Leather"))
                {
                    check2.IsChecked = true;
                }
                if (list.Contains("Toys"))
                {
                    check9.IsChecked = true;
                }
                if (list.Contains("Stationery"))
                {
                    check4.IsChecked = true;
                }
                if (list.Contains("Automobile"))
                {
                    check5.IsChecked = true;
                }
                if (list.Contains("Sportswear"))
                {
                    check6.IsChecked = true;
                }
            }
            else
            {
                SetValue(NavigationPage.HasNavigationBarProperty, false);
            }
        }

       
        private void Button1(object sender, EventArgs e)
        {
            if (check1.IsChecked == false)
            {
                check1.IsChecked = true;
            }
            else
            {
                check1.IsChecked = false;
            }

        }
        private void Button2(object sender, EventArgs e)
        {
            if (check2.IsChecked == false)
            {
                check2.IsChecked = true;
            }
            else
            {
                check2.IsChecked = false;
            }

        }
        private void Button3(object sender, EventArgs e)
        {
            if (check9.IsChecked == false)
            {
                check9.IsChecked = true;
            }
            else
            {
                check9.IsChecked = false;
            }
        }
        private void Button4(object sender, EventArgs e)
        {
            if (check4.IsChecked == false)
            {
                check4.IsChecked = true;
            }
            else
            {
                check4.IsChecked = false;
            }
        }
        private void Button5(object sender, EventArgs e)
        {
            if (check5.IsChecked == false)
            {
                check5.IsChecked = true;
            }
            else
            {
                check5.IsChecked = false;
            }
        }
        private void Button6(object sender, EventArgs e)
        {
            if (check6.IsChecked == false)
            {
                check6.IsChecked = true;
            }
            else
            {
                check6.IsChecked = false;
            }
        }

        // Skip button click
        private async void Labtab(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmation", "Are You Sure ?", "Yes");
            CustomerClass preference = new CustomerClass();
            preference.customer_id = cID;
            preference.preference = "null";
            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(preference);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new NavigationDrawer(Settings.AccessName));
        }

        // Preference button click
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            this.IsBusy = true;
            CustomerClass prefer = new CustomerClass();
           
            if(removeId == 1)
            {
                prefer.customer_id = cID;
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/removePreference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            }

            if (check1.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Knife";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
             
            }

            if (check2.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Leather";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            }

            if (check9.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Toys";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            }

            if (check4.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Stationery";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            }

            if (check5.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Automobile";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            }

            if (check6.IsChecked == true)
            {
                prefer.customer_id = cID;
                prefer.preference = "Sportswear";
                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/preference";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(prefer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            }

            if (removeId == 1)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Preference updated", "Ok");
                Navigation.PopAsync();
            }
            else
            {
                this.IsBusy = false;
                Navigation.PopAsync();
                Navigation.PushAsync(new NavigationDrawer(Settings.AccessName));
            }
        }
    }
}