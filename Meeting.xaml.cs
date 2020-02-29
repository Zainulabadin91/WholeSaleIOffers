using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Meeting : ContentPage
    {
        public static string first;
        public static int verify;
        public static string to;
        public static string from;
        public static string date;
        public static string last;
        public static string email;
        public static string country;
        public static string phone;
        public static int approvedCode;

        public Meeting(string a, string b, string c, string d ,string e, string f,string g )
        {
            InitializeComponent();
            first = a;
            last = b;
            email = c;
            verify =Convert.ToInt32(d);
            approvedCode = Convert.ToInt32(e);
            phone = f;
            country = g;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            to =totime.Time.ToString();
            from = fromtime.Time.ToString();
            date = datep.Date.ToString();
            const string accountSid = "AC616a7e7cb46e4c864a3d9061f4dbde4e";
            const string authToken = "9af49549b7c865d0a8d0eb92adc6b38e";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Dear " + first + " " + last + " this is reminder about your meeting on " + date + " between " + from + " and " + to + " if you need to reschedule your meeting, Please call us at (+92)3066927161",
                from: new Twilio.Types.PhoneNumber("+12024101762"),
                to: new Twilio.Types.PhoneNumber(phone));

            CustomerClass customer = new CustomerClass();

            if (email.Contains("@"))
            {
                customer.email = email;
                customer.user_name = "null";
            }
            else
            {
                customer.email = "null";
                customer.user_name = email;
            }

            customer.first_name = first;
            customer.last_name = last; 
            customer.password = "null";
            customer.verification_code = verify.ToString();
            customer.is_active = 1;
            customer.is_block = 0;
            customer.meeting_date = date;
            customer.meeting_start_time = from;
            customer.meeting_end_time = to;
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
                await DisplayAlert("Message", "Your meeting has been sheduled. Please wait for a call and your password will be updated.", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else if (response.Status == 4)
            {
                await DisplayAlert("Message", "ERROR", "ok");
            }
           
        }
    }
}
