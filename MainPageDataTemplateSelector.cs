using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    public class MainPageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate scoredDataTemplate { get; set; }
        public DataTemplate metaDataTemplate { get; set; }
        public DataTemplate nonmetaDataTemplate { get; set; }
        public DataTemplate debugMsgTemplate { get; set; }
        public DataTemplate continentTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Payloads.FrontpageScoredPayload) return scoredDataTemplate;
            if (item is Payloads.FrontpageMetaPayload) return metaDataTemplate;
            if (item is Payloads.FrontpageNonmetaPayload) return nonmetaDataTemplate;
            if (item is Payloads.DebugPayload) return debugMsgTemplate;
            if (item is Payloads.FrontpageContPayload) return continentTemplate;

            Console.WriteLine("Object item does not equal FrontpagePayload");
            return new DataTemplate();
        }
    }
}
