using System;
#if __IOS__
using CoreGraphics;
using UIKit;
#endif

namespace EOS.UI.Shared.Helpers
{
    public class ShadowConfig
    {
#if __IOS__
        public CGColor Color { get; set; }
        public CGSize Offset { get; set; }
        public nfloat Radius { get; set; }
        public float Opacity { get; set; }
#endif
    }
}
