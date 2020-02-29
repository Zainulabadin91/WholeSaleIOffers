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
    public partial class ResetPassword : ContentPage
    {
        public ResetPassword()
        {
            InitializeComponent();
        }

        private async void Reset_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentPass.Text) || string.IsNullOrEmpty(newPass.Text) || string.IsNullOrEmpty(confirmPass.Text))
            {
                await DisplayAlert("Message", "Please fill all the fields", "ok");
            }
            else
            {
                CustomerClass customer = new CustomerClass();
                customer.customer_id = Convert.ToInt32(Settings.AccessToken);
                customer.password = currentPass.Text;

                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/checkPassword";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                string result = await responseMessage.Content.ReadAsStringAsync();

                Response response = JsonConvert.DeserializeObject<Response>(result);
                if (response.Status == 1)
                {
                    if (newPass.Text != confirmPass.Text)
                    {
                        await DisplayAlert("Message", "New password does not match with the confirm password", "ok");
                    }
                    else
                    {
                        CustomerClass customer1 = new CustomerClass();
                        customer1.customer_id = Convert.ToInt32(Settings.AccessToken);
                        customer1.password = newPass.Text;

                        string url1 = "https://woccloudpublish.azurewebsites.net/api/Customer/updatePassword";
                        HttpClient client1 = new HttpClient();
                        string JsonData1 = JsonConvert.SerializeObject(customer1);
                        StringContent content1 = new StringContent(JsonData1, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseMessage1 = await client1.PostAsync(url1, content1);
                        string result1 = await responseMessage1.Content.ReadAsStringAsync();
                        Response response1 = JsonConvert.DeserializeObject<Response>(result1);

                        await DisplayAlert("Message", "Password updated successfully", "ok");
                        await Navigation.PopAsync();
                    }
                }
                else if (response.Status == 2)
                {
                    await DisplayAlert("Message", "Current password in not correct", "ok");
                }
            }
        }
    }
}