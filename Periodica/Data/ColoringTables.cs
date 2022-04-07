using System;
using System.Collections.Generic;
using Windows.UI;

namespace Bluegrams.Periodica.Data
{
    public static class ColoringTables
    {
        public static Dictionary<int, Color> ByCategoryTable { get; } = new Dictionary<int, Color>()
        {
            {(int)ElementCategory.AlkaliMetal, Colors.LightGreen },
            {(int)ElementCategory.AlkalineEarthMetal, Colors.LightSeaGreen },
            {(int)ElementCategory.NobleGas, Colors.Khaki},
            {(int)ElementCategory.Halogen, Colors.Orange},
            {(int)ElementCategory.Nonmetal, Colors.Tomato},
            {(int)ElementCategory.Metalloid, Colors.MediumOrchid},
            {(int)ElementCategory.PostTransitionMetal, Colors.Violet},
            {(int)ElementCategory.TransitionMetal, Colors.CornflowerBlue},
            {(int)ElementCategory.Lanthanoid, Colors.LightSkyBlue},
            {(int)ElementCategory.Actinoid, Colors.LightBlue},
        };

        public static Dictionary<int, Color> ByBlockTable { get; } = new Dictionary<int, Color>()
        {
            {'s', Colors.Salmon },
            {'p', Colors.PowderBlue},
            {'d', Color.FromArgb(255, 240, 223, 123)},
            {'f', Colors.PaleGreen }
        };

        public static Dictionary<int, Color> ByStateTable { get; } = new Dictionary<int, Color>()
        {
            {(int)StateOfMatter.Solid, Colors.Silver },
            {(int)StateOfMatter.Liquid, Colors.LightSkyBlue},
            {(int)StateOfMatter.Gas, Colors.LightGreen}
        };
    }
}
