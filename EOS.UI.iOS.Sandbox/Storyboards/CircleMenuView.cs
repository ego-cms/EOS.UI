using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuView : BaseViewController
    {
        public const string Identifier = "CircleMenuView";

        public CircleMenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var circleMenu = new CircleMenu(View);
            circleMenu.LeftSwiped += (sender, e) => swipeLabel.Text = "Left swipe";
            circleMenu.RightSwiped += (sender, e) => swipeLabel.Text = "Right swipe";

            circleMenu.Attach();
        }
    }
}