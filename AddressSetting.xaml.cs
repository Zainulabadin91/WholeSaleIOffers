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
    public partial class AddressSetting : ContentPage
    {
        public AddressSetting()
        {
            InitializeComponent();
            globalAddress.Text = Settings.AccessAddress;
            globalShipAddress.Text = Settings.AccessShipAddress;
        }

        private async void newAddress(Object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new Address(2));
        }
    }
}