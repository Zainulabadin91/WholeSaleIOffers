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
    public partial class Verification : ContentPage
    {
        public static string verify;
        public static string first;
        public static string last;
        public static string email;
        public static string phone;
        public static string country;
        public static string cod;
        public static string pass;
        public List<string> dumList = new List<string>();

        public Verification( string a, string b, string c, string p, string d, string e, string f)
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            first = a;
            last = b;
            email = c;
            verify = d;
            phone = e;
            country = f;
            pass = p;
            lbl.Text = "Welcome" + " " + first + " " + last;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            cod = code.Text.ToString();
            if (cod == verify && phone == phone)
            {
                CustomerClass customer = new CustomerClass();

                customer.email = email;
                customer.user_name = "null";
                customer.first_name = first;
                customer.last_name = last;
                customer.password = pass;
                customer.verification_code = verify.ToString();
                customer.is_active = 1;
                customer.is_block = 0;
                customer.phone_number = phone;
                customer.country = country;
                customer.global_id = new Random().Next(1000000, 9999999);

                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/AddUser";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                string result = await responseMessage.Content.ReadAsStringAsync();

                Response response = JsonConvert.DeserializeObject<Response>(result);

                if (response.Status == 2)
                {
                    Settings.AccessToken = response.SessionID.ToString();
                    await DisplayAlert("Message", "Your account has been verified", "OK");
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new Preference(Settings.AccessToken, dumList,2));
                }
            }
            else
            {
                await DisplayAlert("Message", "Invalid verification code", "OK");
            }
        }
    }
}