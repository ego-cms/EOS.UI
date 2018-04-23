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

            var ghostButton = new GhostButton();
            ghostButton.SetTitle("Press me", UIControlState.Normal);
            ghostButton.LetterSpacing = 8;
            ghostButton.TextSize = 20;
            ghostButton.EnabledTextColor = UIColor.Red;
            ghostButton.DisabledTextColor = UIColor.Blue;
            ghostButton.PressedStateTextColor = UIColor.Orange;
            ghostButton.Enabled = false;

            ghostButton.TouchUpInside += (sender, e) => 
            {
                
            };

            containerView.ConstrainLayout(() => ghostButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          ghostButton.Frame.GetCenterY() == containerView.Frame.GetCenterY(), ghostButton);
		}
	}
}