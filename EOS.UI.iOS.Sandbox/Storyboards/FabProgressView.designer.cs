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
	[Register ("FabProgressView")]
	partial class FabProgressView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown backgroundDropDown { get; set; }

		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown disabledColorDropDown { get; set; }

		[Outlet]
		UIKit.UISwitch enableSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown pressedColorDropDown { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowColorDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowOffsetXDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowOffsetYDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowOpacityDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowRadiusDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown sizeDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		EOS.UI.iOS.Sandbox.EOSSandboxDropDown themeDropDown { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (enableSwitch != null) {
				enableSwitch.Dispose ();
				enableSwitch = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}

			if (backgroundDropDown != null) {
				backgroundDropDown.Dispose ();
				backgroundDropDown = null;
			}

			if (disabledColorDropDown != null) {
				disabledColorDropDown.Dispose ();
				disabledColorDropDown = null;
			}

			if (pressedColorDropDown != null) {
				pressedColorDropDown.Dispose ();
				pressedColorDropDown = null;
			}

			if (shadowDropDown != null) {
				shadowDropDown.Dispose ();
				shadowDropDown = null;
			}

			if (sizeDropDown != null) {
				sizeDropDown.Dispose ();
				sizeDropDown = null;
			}

			if (themeDropDown != null) {
				themeDropDown.Dispose ();
				themeDropDown = null;
			}

			if (shadowColorDropDown != null) {
				shadowColorDropDown.Dispose ();
				shadowColorDropDown = null;
			}

			if (shadowOffsetYDropDown != null) {
				shadowOffsetYDropDown.Dispose ();
				shadowOffsetYDropDown = null;
			}

			if (shadowRadiusDropDown != null) {
				shadowRadiusDropDown.Dispose ();
				shadowRadiusDropDown = null;
			}

			if (shadowOpacityDropDown != null) {
				shadowOpacityDropDown.Dispose ();
				shadowOpacityDropDown = null;
			}

			if (shadowOffsetXDropDown != null) {
				shadowOffsetXDropDown.Dispose ();
				shadowOffsetXDropDown = null;
			}
		}
	}
}
