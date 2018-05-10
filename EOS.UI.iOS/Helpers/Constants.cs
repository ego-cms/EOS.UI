using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Helpers
{
    public class Constants
    {
        public static class InputConstants
        {
            public static nfloat IconSize = 24f;
            public static nfloat IconPadding = 12f;
            public static nfloat UnderlineHeight = 1f;
            public static string UnderlineName = "Underline";
        }

        public static Dictionary<string, ShadowConfig> ShadowConfigs = new Dictionary<string, ShadowConfig>()
            {
                {"light", new ShadowConfig(){
                        Color = UIColor.LightGray.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 5,
                        Opacity = 0.7f
                    }},
                {"dark", new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 3,
                        Opacity = 0.2f
                    }}
            };
    }
}
