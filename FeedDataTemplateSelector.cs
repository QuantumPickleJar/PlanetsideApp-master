﻿using System;
using Xamarin.Forms;
namespace PsApp
{
    public class FeedDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate facilityControlTemplate { get; set; }
        public DataTemplate facilityDefendTemplate { get; set; }
        public DataTemplate metagameTemplate { get; set; }
        public DataTemplate nonMetagameTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is VisualCapturePayload) return facilityControlTemplate;
            if (item is VisualDefensePayload) return facilityDefendTemplate;
            if (item is VisualNonmetaPayload) return nonMetagameTemplate;
            if (item is VisualEventPayload) return metagameTemplate;

            Console.WriteLine("Object item does not equal VisualPayload");
            return new DataTemplate();
        }
    }
}
