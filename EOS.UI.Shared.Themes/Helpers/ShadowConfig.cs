using System;
#if __IOS__
using CoreGraphics;
using UIKit;
#else
using Android.Graphics;
#endif

namespace EOS.UI.Shared.Helpers
{
    public class ShadowConfig
    {
#if __IOS__
        public UIColor Color { get; set; }
        public CGPoint Offset { get; set; }
        public nfloat Blur { get; set; }
        public int Spread { get; set; }
#else
        public Color Color { get; set; }
        public Point Offset { get; set; }
        public int Blur { get; set; }
        public int Spread { get; set; }
#endif

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var otherShadowConfig = obj as ShadowConfig;
            if (otherShadowConfig == null)
                return false;

            return Color == otherShadowConfig.Color &&
#if __IOS__
                             Offset == otherShadowConfig.Offset &&
#else
//it's class on Android, can be null
                             Offset!=null && 
                             otherShadowConfig.Offset !=null &&
                             Offset.X == otherShadowConfig.Offset.X &&
                             Offset.Y == otherShadowConfig.Offset.Y &&
#endif
                             Blur == otherShadowConfig.Blur;
        }

        public override int GetHashCode()
        {
            return GetObjectHashCode(Color) + GetObjectHashCode(Offset) + Blur.GetHashCode();
        }

        private int GetObjectHashCode(object obj)
        {
            if (obj == null)
                return 0;
            else
                return obj.GetHashCode();
        }

        public static bool operator ==(ShadowConfig config1, ShadowConfig config2)
        {
            if (object.ReferenceEquals(config1, null))
            {
                return object.ReferenceEquals(config2, null);
            }

            return config1.Equals(config2);
        }

        public static bool operator !=(ShadowConfig config1, ShadowConfig config2)
        {
            if (object.ReferenceEquals(config1, null))
            {
                return !object.ReferenceEquals(config2, null);
            }

            var a = !config1.Equals(config2);
            return a;
        }

    }
}
