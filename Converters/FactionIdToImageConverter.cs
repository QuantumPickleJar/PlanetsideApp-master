using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    class FactionIdToImageConverter : IValueConverter
    {
        public int Faction1Id { get; set; } = 1;
        public ImageSource Faction1ImageSource { get { return ImageSource.FromFile("vs_icon.png"); } }
        
        public int Faction3Id { get; set; } = 2;
        public ImageSource Faction3ImageSource { get { return ImageSource.FromFile("vs_icon.png"); } }

        public int Faction2Id { get; set; } = 3;
        public ImageSource Faction2ImageSource { get { return ImageSource.FromFile("vs_icon.png"); } }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                if (i == Faction1Id)
                    return Faction1ImageSource;
                if (i == Faction2Id)
                    return Faction2ImageSource;
                if (i == Faction3Id)
                    return Faction3ImageSource;
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
            return value;
        }
    }
}
