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
	[Register ("GhostButtonView")]
	partial class GhostButtonView
	{
		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UITextField disabledColorField { get; set; }

		[Outlet]
		UIKit.UITextField enabledColorField { get; set; }

		[Outlet]
		UIKit.UITextField fontField { get; set; }

		[Outlet]
		UIKit.UITextField fontSizeField { get; set; }

		[Outlet]
		UIKit.UITextField letterSpacingField { get; set; }

		[Outlet]
		UIKit.UITextField pressedColorField { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		UIKit.UISwitch stateSwitch { get; set; }

		[Outlet]
		UIKit.UITextField themeField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (disabledColorField != null) {
				disabledColorField.Dispose ();
				disabledColorField = null;
			}

			if (enabledColorField != null) {
				enabledColorField.Dispose ();
				enabledColorField = null;
			}

			if (fontField != null) {
				fontField.Dispose ();
				fontField = null;
			}

			if (fontSizeField != null) {
				fontSizeField.Dispose ();
				fontSizeField = null;
			}

			if (letterSpacingField != null) {
				letterSpacingField.Dispose ();
				letterSpacingField = null;
			}

			if (pressedColorField != null) {
				pressedColorField.Dispose ();
				pressedColorField = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}

			if (themeField != null) {
				themeField.Dispose ();
				themeField = null;
			}

			if (stateSwitch != null) {
				stateSwitch.Dispose ();
				stateSwitch = null;
			}
		}
	}
}
