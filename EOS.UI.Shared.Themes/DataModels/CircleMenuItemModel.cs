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
        
        public CircleMenuItemModel () {}
        
        public CircleMenuItemModel(int id, UIImage imageSource, List<CircleMenuItemModel> children = null)
        {
            Id = id;
            ImageSource = imageSource;
            Children = children ?? new List<CircleMenuItemModel>();
        }
#endif

#if __ANDROID__
        public Drawable ImageSource { get; set; }

        public CircleMenuItemModel()
        {

        }

        public CircleMenuItemModel(int id, Drawable drawable, List<CircleMenuItemModel> children = null)
        {
            Id = id;
            ImageSource = drawable;
            Children = children ?? new List<CircleMenuItemModel>();
        }
#endif
        public List<CircleMenuItemModel> Children { get; set; }

        public bool HasChildren => Children != null && Children.Count > 0;
    }
}
