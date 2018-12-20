using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    class WorldIdToString : IValueConverter
    {
        public int worldId1 { get; set; } = 1;
        public string worldName1 { get; set; } = "Connery";
        
        public int worldId10 { get; set; } = 10;
        public string worldName10 { get; set; } = "Miller";
        
        public int worldId13 { get; set; } = 13;
        public string worldName13 { get; set; } = "Cobalt";
        
        public int worldId17 { get; set; } = 17;
        public string worldName17 { get; set; } = "Emerald";
        
        public int worldId19 { get; set; } = 19;
        public string worldName19 { get; set; } = "Jaeger";
        
        public int worldId25 { get; set; } = 25;
        public string worldName25 { get; set; } = "Briggs";
        
        public int worldId40 { get; set; } = 40;
        public string worldName40 { get; set; } = "Soltech";
        
        public int worldId100 { get; set; } = 100;
        public string worldName100 { get; set; } = "All";


        public string FetchString(int i)
        {
            if (i == worldId1) return worldName1;
            if (i == worldId10) return worldName10;
            if (i == worldId13) return worldName13;
            if (i == worldId17) return worldName17;
            if (i == worldId19) return worldName19;
            if (i == worldId25) return worldName25;
            if (i == worldId40) return worldName40;
            if (i == worldId100) return worldName100;
            else return "ERROR";
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                if (i == worldId1) return worldName1;
                if (i == worldId10) return worldName10;
                if (i == worldId13) return worldName13;
                if (i == worldId17) return worldName17;
                if (i == worldId19) return worldName19;
                if (i == worldId25) return worldName25;
                if (i == worldId40) return worldName40;
                return "ERROR";
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
                return value;
            }
            else
            {
                // cannot convert, return the given value as-is
                return value;
            }
        }
    }
}
