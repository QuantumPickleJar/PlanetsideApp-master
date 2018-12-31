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
        public DataTemplate wgStartDataTemplate { get; set; }
        public DataTemplate wgEndDataTemplate { get; set; }
        public DataTemplate nonmetaDataTemplate { get; set; }
        public DataTemplate debugMsgDataTemplate { get; set; }
        public DataTemplate continentDataTemplate { get; set; }
        public DataTemplate extendedDataTemplate{ get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Payloads.FrontpageScoredPayload) return scoredDataTemplate;
            if (item is Payloads.FrontpageMetaPayload) return metaDataTemplate;
            if (item is Payloads.FrontpageNonmetaPayload) return nonmetaDataTemplate;
            if (item is Payloads.DebugPayload) return debugMsgDataTemplate;
            if (item is Payloads.FrontpageContPayload) return continentDataTemplate;
            if (item is Payloads.FrontpageExtendedPayload) return extendedDataTemplate;
            if (item is Payloads.FrontpageWarpgateStartPayload) return wgStartDataTemplate;
            if (item is Payloads.FrontpageWarpgateEndPayload) return wgEndDataTemplate;

            Console.WriteLine("Object item does not equal FrontpagePayload");
            return new DataTemplate();
        }
    }
}
