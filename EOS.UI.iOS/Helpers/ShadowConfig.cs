using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Helpers
{
    public class ShadowConfig
    {
        public CGColor Color { get; set; }
        public CGSize Offset { get; set; }
        public nfloat Radius { get; set; }
        public float Opacity { get; set; }
    }
}
