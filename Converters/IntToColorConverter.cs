using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    class IntToColorConverter : IValueConverter
    {
        public int Faction1Id { get; set; } = 1;
        public Color Faction1Color { get; set; } = Color.FromRgb(68,15,98);

        public int Faction2Id { get; set; } = 3;
        public Color Faction3Color { get; set; } = Color.FromRgb(0, 75, 128); 

        public int Faction3Id { get; set; } = 2;
        public Color Faction2Color { get; set; } = Color.FromRgb(158, 11, 15);

        public Color DefaultFactionColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                if (i == Faction1Id)
                    return Faction1Color;
                if (i == Faction2Id)
                    return Faction2Color;
                if (i == Faction3Id)
                    return Faction3Color;
                return DefaultFactionColor;
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
