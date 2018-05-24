// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Sandbox
{
	[Register ("CircleProgressView")]
	partial class CircleProgressView
	{
		[Outlet]
		UIKit.UITextField checkmarkColorField { get; set; }

		[Outlet]
		UIKit.UITextField colorField { get; set; }

		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UITextField fontField { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		UIKit.UISwitch showProgressSwitch { get; set; }

		[Outlet]
		UIKit.UITextField textSizeField { get; set; }

		[Outlet]
		UIKit.UITextField themeField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (themeField != null) {
				themeField.Dispose ();
				themeField = null;
			}

			if (fontField != null) {
				fontField.Dispose ();
				fontField = null;
			}

			if (colorField != null) {
				colorField.Dispose ();
				colorField = null;
			}

			if (checkmarkColorField != null) {
				checkmarkColorField.Dispose ();
				checkmarkColorField = null;
			}

			if (textSizeField != null) {
				textSizeField.Dispose ();
				textSizeField = null;
			}

			if (showProgressSwitch != null) {
				showProgressSwitch.Dispose ();
				showProgressSwitch = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}
		}
	}
}
