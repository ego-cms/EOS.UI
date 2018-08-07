using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuItemView : UIViewController
    {
        public UIImage MenuItemImage { get; set; }
        
        public CircleMenuItemView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            menuItemImage.Image = MenuItemImage;
        }
    }
}