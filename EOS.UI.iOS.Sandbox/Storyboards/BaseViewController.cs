using System;
using EOS.UI.iOS.Sandbox.Controls;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public abstract class BaseViewController: UIViewController
    {
        public BaseViewController(IntPtr intPtr): base(intPtr) { }

		public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(false, false);
		}
	}
}
