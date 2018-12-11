using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        private bool _switchState = false;

        public bool GetDevState()
        {
            return _switchState;
        }

        public SettingsPage()
        {
            InitializeComponent();
        }

        public SettingsPage(bool d)
        {
            InitializeComponent();

        }

        private void themePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //broadcast to somewhere that we need to rebuild the pages with the new settings
        }

        private void switchDeveloper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _switchState = switchDeveloper.IsToggled;
            //define OnPropertyChanged for the settings 
        }
    }
}