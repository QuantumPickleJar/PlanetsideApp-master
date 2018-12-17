//using System;
//using System.Collections.Generic;
//using Xamarin.Forms;
//namespace PsApp
//{
//    public class FeedPageDataClass : ContentPage
//    {

//        DataTemplate facilityControlTemplate;
//        DataTemplate facilityDefendTemplate;
//        DataTemplate metagameTemplate;

//        public FeedPageDataClass()
//        {
//            SetupDataTemplates();
//        }

//        void SetupDataTemplates()
//        {
//            facilityDefendTemplate = new DataTemplate(() =>
//            {
//                // how do we implement the converters to this?
//                //example
//                /*
//                 * VS has captured Allatum Bio Lab 
//                 * on Amerish from TR at [epoch]
//                 */
                 
//                FactionIdToStringConverter fS = new FactionIdToStringConverter();
//                IntToColorConverter iC = new IntToColorConverter();
//                Span newFactSpan = new Span(); //"VS"
//                Span text1 = new Span() { Text = " has succesfully defended " };
//                Span facilityNameSpan = new Span(); //"Allatum Bio Lab"
//                Span text2= new Span() { Text = " on " }; // on
//                Span continentSpan = new Span(); // Esamir
//                Span text3 = new Span() { Text = " at " };
//                Span timestampSpan = new Span(); //epoch.AddSeconds(unixTime).ToLongTimeString();                

//                FlexLayout flex = new FlexLayout();

//                Label theLabel = new Label
//                {
//                    FormattedText =
//                    {
//                        Spans =
//                        {
//                            newFactSpan, text1, facilityNameSpan, text2, continentSpan, text3, timestampSpan
//                        }
//                    }
//                };
             
//                newFactSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.new_faction_id", BindingMode.Default, fS, null);
//                newFactSpan.SetBinding(Span.TextColorProperty, "VisualPayload.payload.new_faction_id", BindingMode.Default, iC, null);

//                facilityNameSpan.SetBinding(Span.TextProperty, "VisualPayload.name");
//                continentSpan.SetBinding(Span.TextProperty, "VisualPayload.continent");
//                timestampSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.FromUnixTime");


//                flex.Children.Add(theLabel);
//                return new ViewCell
//                {
//                    View = flex
//                };
//            });

//            facilityControlTemplate = new DataTemplate(() =>
//            {
//                // how do we implement the converters to this?
//                //examplee
//                /*
//                 * VS has captured Allatum Bio Lab from TR at time 
//                 */

//                FactionIdToStringConverter fS = new FactionIdToStringConverter();
//                IntToColorConverter iC = new IntToColorConverter();
//                Span newFactSpan = new Span(); //"VS"
//                Span text1 = new Span() { Text = " has captured " };
//                Span facilityNameSpan = new Span(); //"Allatum Bio Lab
//                Span text2 = new Span() { Text = " from " };
//                Span oldFactSpan = new Span(); //"TR"
//                Span text3 = new Span() { Text = " on " };
//                Span continentSpan = new Span(); //Esamir
//                Span text4 = new Span() { Text = " at " };
//                Span timestampSpan = new Span(); //epoch.AddSeconds(unixTime).ToLongTimeString();                

//                FlexLayout flex = new FlexLayout();

//                Label theLabel = new Label
//                {
//                    FormattedText =
//                    {
//                        Spans =
//                        {
//                            newFactSpan, text1, facilityNameSpan, text2, oldFactSpan, text3, continentSpan, text4, timestampSpan
//                        }
//                    }
//                };
//                newFactSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.new_faction_id", BindingMode.Default, fS, null);
//                newFactSpan.SetBinding(Span.TextColorProperty, "VisualPayload.payload.new_faction_id", BindingMode.Default, iC, null);

//                facilityNameSpan.SetBinding(Span.TextProperty, "VisualPayload.name");

//                oldFactSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.old_faction_id", BindingMode.Default, fS, null);
//                oldFactSpan.SetBinding(Span.TextColorProperty, "VisualPayload.payload.old_faction_id", BindingMode.Default, iC, null);

//                continentSpan.SetBinding(Span.TextProperty, "VisualPayload.continent");
//                timestampSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.FromUnixTime");
                

//                flex.Children.Add(theLabel);
//                return new ViewCell
//                {
//                    View = flex
//                };
//            });

//            metagameTemplate = new DataTemplate(() =>
//            {
                
//                /*
//                 * Aerial Anomalies on Esamir has started
//                 * 
//                 */

//                Span eventNameSpan = new Span(); //Aerial Anomalies                
//                Span text1 = new Span() { Text = " on " };
//                Span eventContSpan= new Span(); //Esamir                
//                Span text2 = new Span() { Text = " has " };
//                Span eventStatusSpan= new Span(); //started
//                Span text3 = new Span() { Text = " at " };
//                Span timestampSpan = new Span(); //epoch.AddSeconds(unixTime).ToLongTimeString();                

//                FlexLayout flex = new FlexLayout();

//                Label theLabel = new Label
//                {
//                    FormattedText =
//                    {
//                        Spans =
//                        {
//                            eventNameSpan, text1, eventContSpan, text2, eventStatusSpan, text3, timestampSpan
//                        }
//                    }
//                };

//                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
//                var dobLabel = new Label();
//                var locationLabel = new Label { HorizontalTextAlignment = TextAlignment.End };

//                eventNameSpan.SetBinding(Span.TextProperty, "VisualPayload.eventName");
//                eventContSpan.SetBinding(Span.TextProperty, "VisualPayload.eventCont");
//                eventStatusSpan.SetBinding(Span.TextProperty, "VisualPayload.eventStatus"); 
//                timestampSpan.SetBinding(Span.TextProperty, "VisualPayload.payload.FromUnixTime");


//                flex.Children.Add(theLabel);
//                return new ViewCell
//                {
//                    View = flex
//                };
//            });
//        }
//    }
//}
