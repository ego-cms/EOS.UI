using System;



namespace EOS.UI.Shared.Themes.Extensions
{
#if __IOS__
    
    public static class ColorExtension
    {
        public static UIKit.UIColor FromHex(string hexString)
        {
            hexString = hexString.ToUpper();
            var hex = hexString.StartsWith("#") ? hexString.Remove(0, 1) : hexString;

            int value = Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            UIKit.UIColor color = null;
            if(hex.Length == 8)
            {
                var red = (nfloat)(((value & 0xFF000000) >> 24) / 255.0);
                var green = (nfloat)(((value & 0x00FF0000) >> 16) / 255.0);
                var blue = (nfloat)(((value & 0x0000FF00) >> 8) / 255.0);
                var alpha = (nfloat)(((value & 0x000000FF)) / 255.0);
                color = new UIKit.UIColor(red, green, blue, alpha);
            }
            if(hex.Length == 6)
            {
                var red = (nfloat)(((value & 0xFF0000) >> 16) / 255.0);
                var green = (nfloat)(((value & 0x00FF00) >> 8) / 255.0);
                var blue = (nfloat)((value & 0x0000FF) / 255.0);
                color = new UIKit.UIColor(red, green, blue, 1.0f);
            }
            return color;
        }
    }
#endif
}
