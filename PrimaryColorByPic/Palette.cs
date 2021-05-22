using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Color = System.Windows.Media.Color;

namespace PrimaryColorByPic
{
    public static class Palette
    {
        public static List<Color> Colors = new List<Color>()
        {
            "#ffcdd2".ToColor(),
            "#e1bee7".ToColor(),
            "#c5cae9".ToColor(),
            "#b3e5fc".ToColor(),
            "#b2dfdb".ToColor(),
            "#dcedc8".ToColor(),
            "#fff9c4".ToColor(),
            "#ffe0b2".ToColor(),
            "#d7ccc8".ToColor(),
            "#cfd8dc".ToColor()
        };

        public static Color GetMostUsedColor(Bitmap bitMap)
        {
            Dictionary<int, int> colorIncidence = new();

            for (int x = 0; x < bitMap.Size.Width; x++)
                for (int y = 0; y < bitMap.Size.Height; y++)
                {
                    int pixelColor = bitMap.GetPixel(x, y).ToArgb();
                    if (colorIncidence.Keys.Contains(pixelColor))
                        colorIncidence[pixelColor]++;
                    else
                        colorIncidence.Add(pixelColor, 1);
                }

            int int32 = colorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value)
                .First().Key;

            string hex = "#"+int32.ToString("X");

            Color color = hex.ToColor();

            return color;
        }

        public static Color GetNearestColor(Color inputColor)
        {
            double inputRed = Convert.ToDouble(inputColor.R);
            double inputGreen = Convert.ToDouble(inputColor.G);
            double inputBlue = Convert.ToDouble(inputColor.B);

            //foreach (object knownColor in Enum.GetValues(typeof(KnownColor)))
            //{
            //    Color color = Color.FromKnownColor((KnownColor) knownColor);
            //    if (!color.IsSystemColor)
            //        colors.Add(color);
            //}

            Color nearestColor = default;
            double distance = 500.0;
            foreach (Color color in Colors)
            {
                // Compute Euclidean distance between the two colors
                double testRed = Math.Pow(Convert.ToDouble(color.R) - inputRed, 2.0);
                double testGreen = Math.Pow(Convert.ToDouble(color.G) - inputGreen, 2.0);
                double testBlue = Math.Pow(Convert.ToDouble(color.B) - inputBlue, 2.0);
                double tempDistance = Math.Sqrt(testBlue + testGreen + testRed);
                if (tempDistance == 0.0)
                    return color;
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    nearestColor = color;
                }
            }

            return nearestColor;
        }

        public static Color ToColor(this string hex)
        {
            return (Color)System.Windows.Media.ColorConverter.ConvertFromString(hex);
        }
    }
}