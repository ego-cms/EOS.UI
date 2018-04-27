using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class InputControlView : BaseViewController
    {
        public const string Identifier = "InputControlView";

        public InputControlView (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLayoutSubviews()
        {
            if(scrollView.ContentSize.Height == 0)
                scrollView.ContentSize = new CoreGraphics.CGSize(scrollView.ContentSize.Width, propertiesContainer.Frame.Height + 220);
            base.ViewDidLayoutSubviews();
        }

    }
}