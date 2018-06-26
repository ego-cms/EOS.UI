using System;
#if __IOS__
using CoreGraphics;
using UIKit;
#endif


namespace EOS.UI.Shared.Themes.DataModels
{
    public class FontStyleItem
    {
#if __IOS__
        public UIFont Font { get; set; }
        public UIColor Color { get; set; }
#endif
        public float Size { get; set; }
        public float LetterSpacing { get; set; }
        public float LineHeight { get; set; }
    }
}
