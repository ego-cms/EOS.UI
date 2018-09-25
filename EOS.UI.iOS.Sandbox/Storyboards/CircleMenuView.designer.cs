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
	[Register ("CircleMenuView")]
	partial class CircleMenuView
	{
		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown focusedButtonColorDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown focusedIconColorDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown itemsCountDropDown { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown themeDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown unfocusedButtonColorDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown unfocusedIconColorDropDown { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (themeDropDown != null) {
				themeDropDown.Dispose ();
				themeDropDown = null;
			}

			if (unfocusedButtonColorDropDown != null) {
				unfocusedButtonColorDropDown.Dispose ();
				unfocusedButtonColorDropDown = null;
			}

			if (focusedButtonColorDropDown != null) {
				focusedButtonColorDropDown.Dispose ();
				focusedButtonColorDropDown = null;
			}

			if (focusedIconColorDropDown != null) {
				focusedIconColorDropDown.Dispose ();
				focusedIconColorDropDown = null;
			}

			if (unfocusedIconColorDropDown != null) {
				unfocusedIconColorDropDown.Dispose ();
				unfocusedIconColorDropDown = null;
			}

			if (itemsCountDropDown != null) {
				itemsCountDropDown.Dispose ();
				itemsCountDropDown = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}
		}
	}
}
