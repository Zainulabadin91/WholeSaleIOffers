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
    public partial class Confirmation : ContentPage
    {
        static string Price;
        static string ShippingCost;
        double Total;
        String myDate = DateTime.Now.ToString();
        DateTime myestimatedDate = DateTime.Now.AddDays(7);

        public Confirmation()
        {
            InitializeComponent();

            this.BindingContext = this;
            this.IsBusy = false;

            globalAddress.Text = Settings.AccessAddress;
            globalShipAddress.Text = Settings.AccessShipAddress;
            //cImage.Source = image;
            //cNumber.Text = number;
            //cName.Text = name;
            label1.Text = Settings.AccessQuantity;
            label2.Text = Settings.AccessPrice + " $";
            Price = Settings.AccessPrice;
            
            ShippingCost = label3.Text;
            Total = (Convert.ToDouble(Price) * Convert.ToDouble(Settings.AccessQuantity)) + Convert.ToDouble(ShippingCost);
            label3.Text = 50 + " $";
            label4.Text = Total.ToString() + " $";
        }

        private async void newAddress(Object sender, EventArgs e)
        {
         
            await Navigation.PushAsync(new Address(1));
        }

        private async void newPaymentDetails(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Payment());
        }

        private async void confirmClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings.AccessBillingAddress1))
            {
                await DisplayAlert("Message", "Please enter your address details first", "OK");
            }
            else
            {
                this.IsBusy = true;
                CustomerClass customer = new CustomerClass();
                customer.customer_id = Convert.ToInt32(Settings.AccessToken);
                customer.item_name = Settings.AccessItemName;
                customer.total_qty = Convert.ToInt32(Settings.AccessQuantity);
                customer.total_amount = Total;
                customer.amount_currency = "$";
                customer.order_date = myDate;
                customer.estimate_delivery_date = myestimatedDate.ToString();
                customer.order_status = "In Process";
                customer.notes = "null";
                customer.is_sync = "null";
                customer.tracking_no = "null";
                customer.glbl_retrn_cust_id = 0;
                customer.billing_address_1 = Settings.AccessBillingAddress1;
                customer.billing_address_2 = Settings.AccessBillingAddress2;
                customer.billing_company = Settings.AccessBillingCompany;
                customer.billing_first_name = Settings.AccessBillingFirstName;
                customer.billing_last_name = Settings.AccessBillingLastName;
                customer.billing_city = Settings.AccessBillingCity;
                customer.billing_state = Settings.AccessBillingState;
                customer.billing_postcode = Settings.AccessBillingPostcode;
                customer.billing_country = Settings.AccessBillingCountry;
                customer.billing_email = Settings.AccessBillingEmail;
                customer.billing_phone = Settings.AccessBillingPhone;
                customer.shipping_address_1 = Settings.AccessShippingAddress1;
                customer.shipping_address_2 = Settings.AccessShippingAddress2;
                customer.shipping_company = Settings.AccessShippingCompany;
                customer.shipping_first_name = Settings.AccessShippingFirstName;
                customer.shipping_last_name = Settings.AccessShippingLastName;
                customer.shipping_city = Settings.AccessShippingCity;
                customer.shipping_state = Settings.AccessShippingState;
                customer.shipping_postcode = Settings.AccessShippingPostcode;
                customer.shipping_country = Settings.AccessShippingCountry;

                string url = "https://woccloudpublish.azurewebsites.net/api/Customer/confirmOrder";
                HttpClient client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                string result = await responseMessage.Content.ReadAsStringAsync();

                this.IsBusy = false;

                await DisplayAlert("Message", "Your order has been confirmed", "OK");
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new NavigationDrawer(Settings.AccessName));
            }
        }
    }
}