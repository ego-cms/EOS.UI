using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class GhostButtonView : BaseViewController
    {
        public const string Identifier = "GhostButtonView";

        public GhostButtonView (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            containerView.BackgroundColor = UIColor.Brown;

            var ghostButton = new GhostButton();
            ghostButton.LetterSpacing = 8;
            ghostButton.EnabledTextColor = UIColor.Red;
            ghostButton.DisabledTextColor = UIColor.Blue;
            ghostButton.SetTitle("Press me", UIControlState.Normal);
            ghostButton.Enabled = false;

            ghostButton.TouchUpInside += (sender, e) => 
            {
                
            };

            containerView.ConstrainLayout(() => ghostButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          ghostButton.Frame.GetCenterY() == containerView.Frame.GetCenterY(), ghostButton);
		}
	}
}