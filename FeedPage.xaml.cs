using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp
{

    //listview that displays object of type Event
   
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedPage : ContentPage
	{
		public FeedPage ()
		{
			InitializeComponent ();
        }


        //example of what a received payload from a playerlogin event subscription would look like in json
//    {
//      "payload":
//        {
//      	"character_id":"5428057349740067905",
//      	"event_name":"PlayerLogin",
//      	"timestamp":"1397251287",
//      	"world_id":"1"
//      },
//      "service":"event",
//      "type":"serviceMessage"
//    }
}

}