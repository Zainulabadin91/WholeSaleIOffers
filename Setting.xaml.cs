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
    public partial class Setting : ContentPage
    {
        public Setting()
        {
            InitializeComponent();
        }

        private async void addressClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressSetting());
        }

        private async void addressImage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressSetting());
        }

        private async void passwordClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassword());
        }

        private async void passwordImage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassword());
        }

        private async void categoryClick(object sender, EventArgs e)
        {
            CustomerClass preference = new CustomerClass();
            preference.customer_id = Convert.ToInt32(Settings.AccessToken);
            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/customerPreference";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(preference);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();
            List<string> myList = (JsonConvert.DeserializeObject<List<string>>(result));


            await Navigation.PushAsync(new Preference(Settings.AccessToken, myList, 1));
        }

        private async void categoryImage(object sender, EventArgs e)
        {
            CustomerClass preference = new CustomerClass();
            preference.customer_id = Convert.ToInt32(Settings.AccessToken);
            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/customerPreference";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(preference);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();
            List<string> myList = (JsonConvert.DeserializeObject<List<string>>(result));


            await Navigation.PushAsync(new Preference(Settings.AccessToken, myList, 1));
        }
    }
}