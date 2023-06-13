using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lojinha

{
    class ThemeColor
    {
        public static Color PrimaryColor { get; set; }
        public static Color SecondaryColor { get; set; }

        public static List<string> ColorList = new List<string>()
        {
            "#73378C",
            "#245CA6",
            "#72C1F2",
            "#80D2F2",
            "#D9B341",
            "#D9501E",
            "#D94436",
            "#8C2727",
            "#BF4572",
            "#F205B3",
            "#93BFB7",
            "#736A6A",
            "#0FC2C0",
            "#0CABA8",
            "#023535",

        };

        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

    }//
}

