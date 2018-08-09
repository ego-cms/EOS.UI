using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuItemView : BaseViewController
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ToggleNavigationBar(true);
        }
    }
}