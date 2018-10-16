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
	public partial class RegisterPersonalDataPage : ContentPage
	{
		public RegisterPersonalDataPage ()
		{
			InitializeComponent ();
		}
        async void BtnContunarRegistro_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPhysicalConditionPage());
        }
    }
}