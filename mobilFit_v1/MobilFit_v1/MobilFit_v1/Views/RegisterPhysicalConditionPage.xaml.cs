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
	public partial class RegisterPhysicalConditionPage : ContentPage
	{
		public RegisterPhysicalConditionPage ()
		{
			InitializeComponent ();
		}

        async void BtnContunarRegistro_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}