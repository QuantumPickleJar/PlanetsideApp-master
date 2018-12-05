using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace PsApp
{
    public class Settings
    {
        public Settings()
        {
            InitializeThemes();
        }

        public bool _IsDeveloperModeOn { get; private set; }

        public string name { get; private set; }
        public Xamarin.Forms.Color textColor { get; private set; }
        public Xamarin.Forms.Color backgroundColor { get; private set; }
        public Xamarin.Forms.Color elementBackgroundColor { get; private set; }
        public Xamarin.Forms.Color buttonColor { get; private set; }
        public Xamarin.Forms.Color buttonTextColor { get; private set; }

        //worry about setting this up later 
        public ImageSource imageSource { get; private set; }

        public void SetFactionThemeVS()
        {
            this.name = "VS";
            //this.buttonColor= new Color(34,101,56);
            //this.buttonTextColor = new Color(214, 214, 214);
            //this.backgroundColor = new Color(95, 20, 143);
            this.imageSource = "http://www.userlogos.org/files/logos/Cracka/PlanetSide-2.png";

        }

        public void SetFactionThemeNC()
        {
            this.name = "NC";
            //this.buttonColor= new Color(34,101,56);
            //this.buttonTextColor = new Color(214, 214, 214);
            //this.backgroundColor = new Color(95, 20, 143);
            this.imageSource = "http://www.userlogos.org/files/logos/Cracka/PlanetSide-2.png";

        }

        public void SetFactionThemeTR()
        {
            this.name = "TR";
            //this.buttonColor= new Color(34,101,56);
            //this.buttonTextColor = new Color(214, 214, 214);
            //this.backgroundColor = new Color(95, 20, 143);
            this.imageSource = "http://www.userlogos.org/files/logos/Cracka/PlanetSide-2.png";

        }


        public void InitializeThemes()
        {
            //use bindings to set these later
            this.name = "default";
            this.textColor = new Color(87, 165, 173);
            this.imageSource = "http://www.userlogos.org/files/logos/Cracka/PlanetSide-2.png";
            this.buttonTextColor = new Color(214, 214, 214);
        }
    }
}
