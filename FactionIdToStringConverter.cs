using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    class FactionIdToStringConverter : IValueConverter
    {
        public int Faction1Id { get; set; } = 1;
        public string Faction1String { get; set; } = "VS";
        
        public int Faction2Id { get; set; } = 2;
        public string Faction2String { get; set; } = "TR";
        
        public int Faction3Id { get; set; } = 3;
        public string Faction3String { get; set; } = "NC";

       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                if (i == Faction1Id)
                    return Faction1String;
                if (i == Faction2Id)
                    return Faction2String;
                if (i == Faction3Id)
                    return Faction3String;
                return "Unknown faction id";
            }
            else
            {
                // cannot convert, return the given value as-is
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string i)
            {
                if (i == "VS")
                    return Faction1Id;
                if (i == "TR")
                    return Faction2Id;
                if (i == "NC")
                    return Faction3Id;
                return "Unknown faction string";
            }
            else
            {
                // cannot convert, return the given value as-is
                return value;
            }
        }
    }
}
