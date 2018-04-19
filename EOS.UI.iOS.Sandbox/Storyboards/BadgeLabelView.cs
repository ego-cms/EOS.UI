using EOS.UI.iOS.Sandbox.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        public BadgeLabelView (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            var label = new BadgeLabel();
            label.BackgroundColor = UIColor.Red;
            label.TextColor = UIColor.White;
            label.CornerRadius = 5;
            label.Text = "Some text";
            label.LetterSpacing = 1;
            label.UpdateAppearance();

            var provider = label.GetThemeProvider();

            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);
		}
	}
}