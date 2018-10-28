using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BottomBar.XamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilFit_v1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMainMenuPage : BottomBarPage
	{
		public UserMainMenuPage ()
		{
			InitializeComponent ();
		}

        private void BtnEjercicios_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }

        private void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void ContentPage_ChildAdded(object sender, ElementEventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}