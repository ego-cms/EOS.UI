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
    }
}
