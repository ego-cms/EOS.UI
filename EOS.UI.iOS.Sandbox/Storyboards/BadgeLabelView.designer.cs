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
	[Register ("BadgeLabelView")]
	partial class BadgeLabelView
	{
		[Outlet]
		UIKit.UIButton applyButton { get; set; }

		[Outlet]
		UIKit.UIPickerView colorPicker { get; set; }

		[Outlet]
		UIKit.UISegmentedControl colorSegmentedControl { get; set; }

		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UITextField cornerRadiusField { get; set; }

		[Outlet]
		UIKit.UIPickerView fontPicker { get; set; }

		[Outlet]
		UIKit.UITextField fontSizeField { get; set; }

		[Outlet]
		UIKit.UITextField letterSpacingField { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (applyButton != null) {
				applyButton.Dispose ();
				applyButton = null;
			}

			if (colorPicker != null) {
				colorPicker.Dispose ();
				colorPicker = null;
			}

			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (cornerRadiusField != null) {
				cornerRadiusField.Dispose ();
				cornerRadiusField = null;
			}

			if (fontPicker != null) {
				fontPicker.Dispose ();
				fontPicker = null;
			}

			if (fontSizeField != null) {
				fontSizeField.Dispose ();
				fontSizeField = null;
			}

			if (letterSpacingField != null) {
				letterSpacingField.Dispose ();
				letterSpacingField = null;
			}

			if (colorSegmentedControl != null) {
				colorSegmentedControl.Dispose ();
				colorSegmentedControl = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}
		}
	}
}
