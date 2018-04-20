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
		UIKit.UITextField backgroundColorField { get; set; }

		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UITextField cornerRadiusField { get; set; }

		[Outlet]
		UIKit.UITextField fontColorField { get; set; }

		[Outlet]
		UIKit.UITextField fontField { get; set; }

		[Outlet]
		UIKit.UITextField fontSizeField { get; set; }

		[Outlet]
		UIKit.UITextField letterSpacingField { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		UIKit.UITextField themeField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}

			if (backgroundColorField != null) {
				backgroundColorField.Dispose ();
				backgroundColorField = null;
			}

			if (fontField != null) {
				fontField.Dispose ();
				fontField = null;
			}

			if (fontColorField != null) {
				fontColorField.Dispose ();
				fontColorField = null;
			}

			if (letterSpacingField != null) {
				letterSpacingField.Dispose ();
				letterSpacingField = null;
			}

			if (cornerRadiusField != null) {
				cornerRadiusField.Dispose ();
				cornerRadiusField = null;
			}

			if (fontSizeField != null) {
				fontSizeField.Dispose ();
				fontSizeField = null;
			}

			if (themeField != null) {
				themeField.Dispose ();
				themeField = null;
			}
		}
	}
}
