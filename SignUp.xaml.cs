using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Woc.Helpers;
using System.Text.RegularExpressions;
using Rg.Plugins.Popup.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public static string code;
        public static string first;
        public static string last;
        public static string mob;

        public SignUp(string firstname, string lasttname, string useremail, string pass, string phonenumber, string country)
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            this.BindingContext = this;
            this.IsBusy = false;
            fname.Text = firstname;
            lname.Text = lasttname;
            emailID.Text = useremail;
            password.Text = pass;
            phone.Text = phonenumber;
            chooseCountry.Text = country;
        }

        private async void countryClick(Object sender, EventArgs e)
        {
            Settings.AccessFirstName = fname.Text;
            Settings.AccessLastName = lname.Text;
            Settings.AccessEmail = emailID.Text;
            Settings.AccessPhone = phone.Text;
            Settings.AccessPassword = password.Text;
            Navigation.PopAsync();
            var page = new CountryList();

            await PopupNavigation.Instance.PushAsync(page);
        }

        //Register button click
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            this.IsBusy = true;
            CustomerClass customer = new CustomerClass();
            customer.first_name = fname.Text;
            customer.last_name = lname.Text;
            customer.email = emailID.Text;
            customer.phone_number = phone.Text;
            customer.country = chooseCountry.Text;
            Settings.AccessName = fname.Text;

            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/emailCheck";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();
            
            Response response = JsonConvert.DeserializeObject<Response>(result);

            if (response.Status == 0)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Please fill all the fields", "ok");
            }
            else if (response.Status == 4)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Email format is incorrect", "ok");
            }
            else if (response.Status == 1)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Email/Username already exists", "ok");
              

            }
            else if (response.Status == 5)
            {
                this.IsBusy = false;

                code = new Random().Next(1000, 9999).ToString();
                first = fname.Text.ToString();
                last = lname.Text.ToString();
                mob = phone.Text.ToString();
                const string accountSid = "AC616a7e7cb46e4c864a3d9061f4dbde4e";
                const string authToken = "9af49549b7c865d0a8d0eb92adc6b38e";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Your verification code is" + code,
                    from: new Twilio.Types.PhoneNumber("+12024101762"),
                    to: new Twilio.Types.PhoneNumber(mob)
                );
                Navigation.PushAsync(new Verification(first, last, emailID.Text, password.Text, code, mob, chooseCountry.Text));
            }
        }
    }
}

        