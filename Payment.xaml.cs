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
    public partial class Payment : ContentPage
    {
        bool radio, radio1;
        public static string num;
        public static string img;
        public static string name;

        public Payment()
        {
            InitializeComponent();
            radio = false;
            radio1 = false;
        }

        public void masterButtonTapped(object sender, EventArgs e)
        {
            radio = true;
            card.Source = "checked.png";

            if (radio1.Equals(true))
            {
                card1.Source = "unchecked.png";
                radio1 = false;
            }
        }

        public void visaButtonTapped(object sender, EventArgs e)
        {
            radio1 = true;
            card1.Source = "checked.png";

            if (radio.Equals(true))
            {
                card.Source = "unchecked.png";
                radio = false;
            }
        }

        public async void PaymentSave(object sender, EventArgs e)
        {
            if(radio == true && !string.IsNullOrEmpty(cardNumber.Text) && !string.IsNullOrEmpty(expireDate.Text) && !string.IsNullOrEmpty(cvv.Text) && !string.IsNullOrEmpty(cardholder.Text))
            {
                num = cardNumber.Text;
                img = "mastercard.png";
                name = "MasterCard";

                Settings.AccessCardNumber = num;
                Settings.AccessCardImage = img;
                Settings.AccessCardName = name;

                await DisplayAlert("Message", "Payment Details Updated", "OK");

                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new Confirmation());
            }
            else if(radio1 == true && !string.IsNullOrEmpty(cardNumber.Text) && !string.IsNullOrEmpty(expireDate.Text) && !string.IsNullOrEmpty(cvv.Text) && !string.IsNullOrEmpty(cardholder.Text))
            {
                num = cardNumber.Text;
                img = "visa.png";
                name = "VISA";

                Settings.AccessCardNumber = num;
                Settings.AccessCardImage = img;
                Settings.AccessCardName = name;

                await DisplayAlert("Message", "Payment Details Updated", "OK");

                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new Confirmation());
            }
            else
            {
                await DisplayAlert("Message", "Please fill all the details", "OK");
            }
        }

    }
}