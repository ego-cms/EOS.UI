using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Themes;
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

            var theme = EOSThemeProvider.Instance.GetCurrentTheme();
            menuItemImage.TintColor = theme is LightEOSTheme ? UIColor.Black : UIColor.White;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ToggleNavigationBar(true);
            NavigationController.InteractivePopGestureRecognizer.Enabled = false;
        }
    }
}