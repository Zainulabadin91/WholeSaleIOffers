using Com.OneSignal;
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
    public partial class NavigationDrawer : MasterDetailPage
    {

        public List<MasterPageItems> menuList { get; set; }

        public NavigationDrawer(string name)
        {
            InitializeComponent();

            username.Text = name;

            menuList = new List<MasterPageItems>();

            SetValue(NavigationPage.HasNavigationBarProperty, false);

            //Fot Android / IOS icons
            var page1 = new MasterPageItems() { id = 1, Title = "Home", Icon = "home" };
            var page2 = new MasterPageItems() { id = 2, Title = "My orders", Icon = "myorders" };
            var page3 = new MasterPageItems() { id = 3, Title = "Notifications", Icon = "notification" };
            var page4 = new MasterPageItems() { id = 4, Title = "Settings", Icon = "settings" };
            var page5 = new MasterPageItems() { id = 5, Title = "Contact Us", Icon = "contactus" };
            var page6 = new MasterPageItems() { id = 6, Title = "About Us", Icon = "aboutus" };
            var page7 = new MasterPageItems() { id = 7, Title = "Log Out", Icon = "logout" };

            menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);
            menuList.Add(page6);
            menuList.Add(page7);

            navigationDrawerList.ItemsSource = menuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
        }

        async private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {


            var myselecteditem = e.Item as MasterPageItems;

            switch (myselecteditem.id)
            {

                case 1:
                
                    break;
                case 2:
                    await Navigation.PushAsync(new ContactUs());

                    break;
                case 3:
                    await Navigation.PushAsync(new ContactUs());

                    break;
                case 4:
                    await Navigation.PushAsync(new Setting());

                    break;
                case 5:
                    await Navigation.PushAsync(new ContactUs());

                    break;
                case 6:
                    await Navigation.PushAsync(new ContactUs());

                    break;
                case 7:
                    OneSignal.Current.DeleteTag("user");
                    Settings.AccessToken = "";
                    Settings.AccessName = "";

                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new MainPage());
                    break;

            }
           ((ListView)sender).SelectedItem = null;
            IsPresented = false;


        }
    }
}