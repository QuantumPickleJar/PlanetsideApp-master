using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Text;
namespace PsApp
{
    public class FeedDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate facilityControlTemplate { get; set; }
        public DataTemplate facilityDefendTemplate { get; set; }
        public DataTemplate metagameTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            //make an object for payload of each of these three types?

            //not sure how this is going to work...
            if (item.Equals(typeof(VisualCapturePayload))) return facilityControlTemplate;
            if (item.Equals(typeof(VisualDefensePayload))) return facilityDefendTemplate;
            if (item.Equals(typeof(VisualEventPayload))) return metagameTemplate;

                //}if (item.Equals(typeof(VisualPayload)))
                //{
                //    //the the action being done to the facility is defending
                //    if (item.event_id == null && p.facilityAction == "defended") return facilityDefendTemplate;

                //    //the the action being done to the facility is capturing
                //    if (p.event_id == null && p.facilityAction == "captured") return facilityControlTemplate;

                //    //if there is an event_id attached 
                //    if (p.event_id != null) return metagameTemplate;
                //    return null;
            
            Console.WriteLine("Object item does not equal VisualPayload");
            return null;
        }
    }

}
