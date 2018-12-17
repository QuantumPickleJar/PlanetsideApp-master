using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FisuPage : ContentPage
	{
		public FisuPage ()
		{
			InitializeComponent ();
		}

        private async void fisuPop_ClickedAsync(object sender, EventArgs e)
        {
            FisuPopPage fisu = new FisuPopPage();
            await Navigation.PushAsync(fisu);
        }

    }
}