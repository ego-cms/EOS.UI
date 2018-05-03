using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class FabProgressView : BaseViewController
    {
        public const string Identifier = "FabProgressView";
        private const int _buttonSide = 50;

        public FabProgressView (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            var fab = new FabProgress();

            fab.TouchUpInside += (sender, e) => 
            {
                
            };

            containerView.ConstrainLayout(() => fab.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          fab.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                          fab.Frame.Height == _buttonSide &&
                                          fab.Frame.Width == _buttonSide, fab);
		}
	}
}