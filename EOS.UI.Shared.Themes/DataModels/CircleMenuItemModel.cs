using System;
using System.Collections.Generic;
#if __IOS__
using UIKit;
#endif
#if __ANDROID__
using Android.Graphics.Drawables;
#endif

namespace EOS.UI.Shared.Themes.DataModels
{
    public class CircleMenuItemModel
    {
        public int Id { get; set; }
#if __IOS__
        public UIImage ImageSource { get; set; }
#endif

#if __ANDROID__
        public Drawable ImageSource { get; set; }
#endif
        IEnumerable<CircleMenuItemModel> Children { get; set; }
    }
}
