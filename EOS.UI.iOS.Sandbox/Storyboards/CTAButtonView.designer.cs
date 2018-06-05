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
	[Register ("CTAButtonView")]
	partial class CTAButtonView
	{
		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown cornerRadiusDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown disabledBackgroundDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown disabledTextColorDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown enabledBackgroundDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown enabledTextColorDropDown { get; set; }

		[Outlet]
		UIKit.UISwitch enableSwitch { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown fontDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown letterSpacingDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown pressedBackgroundDropdown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown pressedTextColorDropDown { get; set; }

		[Outlet]
		UIKit.UIButton resetButton { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown textSizeDropDown { get; set; }

		[Outlet]
		EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (themeDropDown != null) {
				themeDropDown.Dispose ();
				themeDropDown = null;
			}

			if (fontDropDown != null) {
				fontDropDown.Dispose ();
				fontDropDown = null;
			}

			if (letterSpacingDropDown != null) {
				letterSpacingDropDown.Dispose ();
				letterSpacingDropDown = null;
			}

			if (textSizeDropDown != null) {
				textSizeDropDown.Dispose ();
				textSizeDropDown = null;
			}

			if (enabledTextColorDropDown != null) {
				enabledTextColorDropDown.Dispose ();
				enabledTextColorDropDown = null;
			}

			if (disabledTextColorDropDown != null) {
				disabledTextColorDropDown.Dispose ();
				disabledTextColorDropDown = null;
			}

			if (pressedTextColorDropDown != null) {
				pressedTextColorDropDown.Dispose ();
				pressedTextColorDropDown = null;
			}

			if (enabledBackgroundDropDown != null) {
				enabledBackgroundDropDown.Dispose ();
				enabledBackgroundDropDown = null;
			}

			if (disabledBackgroundDropDown != null) {
				disabledBackgroundDropDown.Dispose ();
				disabledBackgroundDropDown = null;
			}

			if (pressedBackgroundDropdown != null) {
				pressedBackgroundDropdown.Dispose ();
				pressedBackgroundDropdown = null;
			}

			if (cornerRadiusDropDown != null) {
				cornerRadiusDropDown.Dispose ();
				cornerRadiusDropDown = null;
			}

			if (resetButton != null) {
				resetButton.Dispose ();
				resetButton = null;
			}

			if (enableSwitch != null) {
				enableSwitch.Dispose ();
				enableSwitch = null;
			}
		}
	}
}
