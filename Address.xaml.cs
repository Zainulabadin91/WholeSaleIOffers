using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woc.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Address : ContentPage
    {
        public static string myAddress;
        public static string myShipAddress;
        public static int check;
        public Address(int id)
        {
            InitializeComponent();
            check = id;
        }

            // Address save button click
            public async void saveAddress(object sender, EventArgs e)
        {
            
            if(string.IsNullOrEmpty(country.Text) || string.IsNullOrEmpty(address1.Text) || string.IsNullOrEmpty(city.Text) || string.IsNullOrEmpty(zipCode.Text) || string.IsNullOrEmpty(province.Text))
            {
               await DisplayAlert("Error", "Please fill all the fields", "OK");
            }
            else
            {
                string aCountry = country.Text;
                string aAddress = address1.Text;
                string aCity = city.Text;
                string aZipCode = zipCode.Text;
                string aProvince = province.Text;

                string shipFName;
                string shipLName;
                string shipAddress1;
                string shipAddress2;
                string shipCompany;
                string shipCity;
                string shipProvince;
                string shipCountry;
                string shipZipCode;


                if (ship_check.IsChecked)
                {
                    ship_fname.Text = fname.Text;
                    ship_lname.Text = lname.Text;
                    ship_address1.Text = aAddress;
                    ship_address2.Text = address2.Text;
                    ship_company.Text = company.Text;
                    ship_city.Text = aCity;
                    ship_province.Text = aProvince;
                    ship_country.Text = aCountry;
                    ship_zipCode.Text = aZipCode;

                    shipFName = ship_fname.Text;
                    shipLName = ship_lname.Text;
                    shipAddress1 = ship_address1.Text;
                    shipAddress2 = ship_address2.Text;
                    shipCompany = ship_company.Text;
                    shipCity = ship_city.Text;
                    shipProvince = ship_province.Text;
                    shipCountry = ship_country.Text;
                    shipZipCode = ship_zipCode.Text;

                    myShipAddress = "Shipping: " + shipAddress1 + ", " + shipCity + ", " + shipProvince + ", " + shipCountry;
                    Settings.AccessShippingFirstName = shipFName;
                    Settings.AccessShippingLastName = shipLName;
                    Settings.AccessShippingAddress1 = shipAddress1;
                    Settings.AccessShippingAddress2 = shipAddress2;
                    Settings.AccessShippingCompany = shipCompany;
                    Settings.AccessShippingCity = shipCity;
                    Settings.AccessShippingState = shipProvince;
                    Settings.AccessShippingCountry = shipCountry;
                    Settings.AccessShippingPostcode = shipZipCode;
                }
                else
                {
                    myShipAddress = "Shipping: " + ship_address1.Text + ", " + ship_city.Text + ", " + ship_province.Text + ", " + ship_country.Text;
                    Settings.AccessShippingFirstName = ship_fname.Text;
                    Settings.AccessShippingLastName = ship_lname.Text;
                    Settings.AccessShippingAddress1 = ship_address1.Text;
                    Settings.AccessShippingAddress2 = ship_address2.Text;
                    Settings.AccessShippingCompany = ship_company.Text;
                    Settings.AccessShippingCity = ship_city.Text;
                    Settings.AccessShippingState = ship_province.Text;
                    Settings.AccessShippingCountry = ship_country.Text;
                    Settings.AccessShippingPostcode = ship_zipCode.Text;
                }

                myAddress = "Billing: " + aAddress + ", " + aCity + ", " + aProvince + ", " + aCountry;
                Settings.AccessBillingFirstName = fname.Text;
                Settings.AccessBillingLastName = lname.Text;
                Settings.AccessBillingEmail = email.Text;
                Settings.AccessBillingPhone = phone.Text;
                Settings.AccessBillingAddress1 = address1.Text;
                Settings.AccessBillingAddress2 = address2.Text;
                Settings.AccessBillingCompany = company.Text;
                Settings.AccessBillingCountry = country.Text;
                Settings.AccessBillingCity = city.Text;
                Settings.AccessBillingState = province.Text;
                Settings.AccessBillingPostcode = zipCode.Text;

                Settings.AccessAddress = myAddress;
                Settings.AccessShipAddress = myShipAddress;

                await DisplayAlert("Message", "Address Updated", "OK");

                 Navigation.PopAsync();
                 Navigation.PopAsync();
                if (check == 1)
                {
                    await Navigation.PushAsync(new Confirmation());
                }
                else 
                {
                    await Navigation.PushAsync(new AddressSetting());
                }
            }
        }
    }
}