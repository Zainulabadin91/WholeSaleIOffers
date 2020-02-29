using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Woc.Helpers;

namespace Woc
{
    public partial class App : Application
    {
        static int state;
        public static string productLabel;
        public static string imageURL;
        public static string description;
        public static string ytLink;
        public static string minQuantity;
        public static string offerPrice1;
        public static string offerPrice2;
        public static string offerPrice3;
        public static string offerPrice4;
        public static string offerQuantity2;
        public static string offerQuantity3;
        public static string offerQuantity4;
        public static string deviceID;
        public static string test;
        public static string check;

        public App()
        {

            InitializeComponent();
            OneSignal.Current.StartInit("9318e841-2aa3-45de-a842-76222516647a").HandleNotificationReceived(HandleNotificationReceived).HandleNotificationOpened(HandleNotificationOpened)
             .EndInit();
            OneSignal.Current.IdsAvailable(IdsAvailable);
          
            check = Settings.AccessToken.ToString();
            
            // OneSignal Notification Handlers
            if(check == "")
            {
                MainPage = new NavigationPage(new MainPage());
              
            }
            else
            {
                OneSignal.Current.SendTag("user", "logged_in");
                if (state == 1)
                {
                    MainPage = new NavigationPage(new PushNotification());
                  

                    MessagingCenter.Send(this, "MyLabel", productLabel);
                    MessagingCenter.Send(this, "MyimageURL", imageURL);
                    MessagingCenter.Send(this, "Mydescription", description);
                    MessagingCenter.Send(this, "MyytLink", ytLink);
                    MessagingCenter.Send(this, "MyminQuantity", minQuantity);
                    MessagingCenter.Send(this, "MyofferPrice1", offerPrice1);
                    MessagingCenter.Send(this, "MyofferPrice2", offerPrice2);
                    MessagingCenter.Send(this, "MyofferPrice3", offerPrice3);
                    MessagingCenter.Send(this, "MyofferPrice4", offerPrice4);
                    MessagingCenter.Send(this, "MyofferQuantity2", offerQuantity2);
                    MessagingCenter.Send(this, "MyofferQuantity3", offerQuantity3);
                    MessagingCenter.Send(this, "MyofferQuantity4", offerQuantity4);
                    MessagingCenter.Send(this, "deviceID", deviceID);
                }
                else
                {
                    MainPage = new NavigationPage(new NavigationDrawer(Settings.AccessName));
                  
                }
            }
        }


        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
           
        }

        //OneSignal IDAvailable Method
        private void IdsAvailable(string userID, string pushToken)
        {
            deviceID = userID;
        }

        //OneSignal Notification Received Method
        private static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;

            test = message;
           
        }

        //OneSignal Notification Opening Handler
        public static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;

            if (additionalData != null)
            {
                
                if (additionalData.ContainsKey("imageURL"))
                {

                    imageURL = additionalData["imageURL"].ToString();

                }
                if (additionalData.ContainsKey("description"))
                {

                    description = additionalData["description"].ToString();

                }
                if (additionalData.ContainsKey("ytLink"))
                {

                    ytLink = additionalData["ytLink"].ToString();

                }
                if (additionalData.ContainsKey("productLabel"))
                {

                    productLabel = additionalData["productLabel"].ToString();

                }
                if (additionalData.ContainsKey("minQuantity"))
                {

                    minQuantity = additionalData["minQuantity"].ToString();

                }
                if (additionalData.ContainsKey("offerPrice1"))
                {

                    offerPrice1 = additionalData["offerPrice1"].ToString();

                }
                if (additionalData.ContainsKey("offerPrice2"))
                {

                    offerPrice2 = additionalData["offerPrice2"].ToString();

                }
                if (additionalData.ContainsKey("offerQuantity2"))
                {

                    offerQuantity2 = additionalData["offerQuantity2"].ToString();

                }
                if (additionalData.ContainsKey("offerPrice3"))
                {

                    offerPrice3 = additionalData["offerPrice3"].ToString();

                }
                if (additionalData.ContainsKey("offerQuantity3"))
                {

                    offerQuantity3 = additionalData["offerQuantity3"].ToString();

                }
                if (additionalData.ContainsKey("offerPrice4"))
                {

                    offerPrice4 = additionalData["offerPrice4"].ToString();

                }
                if (additionalData.ContainsKey("offerQuantity4"))
                {

                    offerQuantity4 = additionalData["offerQuantity4"].ToString();

                }
            }
            state = 1;

        }
    }
}
