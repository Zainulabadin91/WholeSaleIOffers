
using Newtonsoft.Json;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Woc.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Woc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PushNotification : ContentPage
    {
        bool isExpanded = false;
        static int Quantity;
        static int Price;
        static int MinQty;
        string youtubeLink;
        

        public PushNotification()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);

            this.BindingContext = this;
            this.IsBusy = false;

            quantity.Text = minQty.Text;

            MessagingCenter.Subscribe<App, string>(this, "MyLabel", (sender, e) =>
            {
                Label1.Text = e;
            }); 

            MessagingCenter.Subscribe<App, string>(this, "MyimageURL", (sender, e) =>
            {
                imageURL.Source = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "Mydescription", (sender, e) =>
            {
                DesText.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyytLink", (sender, e) =>
            {
                youtubeLink = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyminQuantity", (sender, e) =>
            {
                minQty.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferPrice1", (sender, e) =>
            {
                OPLbl.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferPrice2", (sender, e) =>
            {
                OPLbl1.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferPrice3", (sender, e) =>
            {
                OPLbl2.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferPrice4", (sender, e) =>
            {
                OPLbl3.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferQuantity2", (sender, e) =>
            {
                OQLbl1.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferQuantity3", (sender, e) =>
            {
                OQLbl2.Text = e;
            });

            MessagingCenter.Subscribe<App, string>(this, "MyofferQuantity4", (sender, e) =>
            {
                OQLbl3.Text = e;
            });
        }

        //Thumbs up image handling 
        private async void ImgClickU(Object sender, EventArgs e)
        {
            this.IsBusy = true;
            CustomerClass customer = new CustomerClass();
            customer.customer_id = Convert.ToInt32(Settings.AccessToken);
            customer.Product_Name = Label1.Text;
            customer.Thumbs_Up = 1;

            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/UserFeedback";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();

            Response response = JsonConvert.DeserializeObject<Response>(result);
            if (string.IsNullOrEmpty(Settings.AccessToken.ToString()))
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "You have to login first", "ok");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MainPage());
            }
            else if (response.Status == 1)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Your feedback has already been received", "ok");
            }
            else if (response.Status == 2)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Thanks for your feedback", "ok");
            }
            
        }


        //Thumbs down image handling 
        private async void ImgClickD(Object sender, EventArgs e)
        {
            this.IsBusy = true;
            CustomerClass customer = new CustomerClass();
            customer.customer_id = Convert.ToInt32(Settings.AccessToken);
            customer.Product_Name = Label1.Text;
            customer.Thumbs_Down = 1;

            string url = "https://woccloudpublish.azurewebsites.net/api/Customer/UserFeedback";
            HttpClient client = new HttpClient();
            string JsonData = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            string result = await responseMessage.Content.ReadAsStringAsync();

            Response response = JsonConvert.DeserializeObject<Response>(result);
            if (string.IsNullOrEmpty(Settings.AccessToken.ToString()))
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "You have to login first", "ok");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MainPage());
            }
            else if (response.Status == 1)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Your feedback has already been received", "ok");
            }
            else if (response.Status == 2)
            {
                this.IsBusy = false;
                await DisplayAlert("Message", "Thanks for your feedback", "ok");
            }
        }

        // Description button click
        private async void DesClick(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                await Description.FadeTo(0);
                Description.IsVisible = !isExpanded;
                Arrow.Source = "rightArrow.png";
            }
            else
            {
                Description.IsVisible = !isExpanded;
                await Description.FadeTo(1);
                Arrow.Source = "downArrow.png";
            }

            isExpanded = !isExpanded;
        }

        // Description icon click 
        private async void DesImage(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                await Description.FadeTo(0);
                Description.IsVisible = !isExpanded;
                Arrow.Source = "rightArrow.png";
            }
            else
            {
                Description.IsVisible = !isExpanded;
                await Description.FadeTo(1);
                Arrow.Source = "downArrow.png";
            }

            isExpanded = !isExpanded;
            
        }

        // Youtube click
        private async void youtubeClick(Object sender, EventArgs e)
        {
            await CrossShare.Current.OpenBrowser(youtubeLink);
        }

        // Share click
        private async void shareClick(Object sender, EventArgs e)
        {
            await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = Label1.Text,
                Title = "Share",
                Url = youtubeLink
            }); 
        }

        // Buy button click
        private async void buyClick(object sender, EventArgs e)
        {
            MinQty = Convert.ToInt32(minQty.Text);
            Quantity = Convert.ToInt32(quantity.Text);
            Settings.AccessItemName = Label1.Text;
            if(Quantity < MinQty)
            {
               await DisplayAlert("Message", "Quantity can not be less than the minmum given quantity", "OK");
                return;
            }
            else if (Quantity <= Convert.ToInt32(OQLbl1.Text))
            {
                Price = Convert.ToInt32(OPLbl.Text);
             
            }
            else if(Quantity <= Convert.ToInt32(OQLbl2.Text) && Quantity > Convert.ToInt32(OQLbl1.Text))
            {
                Price = Convert.ToInt32(OPLbl1.Text);
              
            }
            else if (Quantity <= Convert.ToInt32(OQLbl3.Text) && Quantity > Convert.ToInt32(OQLbl2.Text))
            {
                Price = Convert.ToInt32(OPLbl2.Text);
               
            }
            else if (Quantity > Convert.ToInt32(OQLbl3.Text))
            {
                Price = Convert.ToInt32(OPLbl3.Text);
                
            }
            Settings.AccessQuantity = Quantity.ToString();
            Settings.AccessPrice = Price.ToString();
            await Navigation.PushAsync(new Confirmation());
        }
    }
}