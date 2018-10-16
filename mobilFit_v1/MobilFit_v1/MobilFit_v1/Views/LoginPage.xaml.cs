using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilFit_v1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}
        private void BtnAcceder_Clicked1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
        }

        async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
    }
}