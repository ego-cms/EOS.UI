// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Sandbox
{
    [Register ("GhostButtonView")]
    partial class GhostButtonView
    {
        [Outlet]
        UIKit.UIView containerView { get; set; }


        [Outlet]
        UIKit.UIButton resetButton { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown rippleColorDropDown { get; set; }


        [Outlet]
        UIKit.UISwitch stateSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown disabledTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown enabledTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown fontDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown letterSpacingDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown pressedTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown textSizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (disabledTextColorDropDown != null) {
                disabledTextColorDropDown.Dispose ();
                disabledTextColorDropDown = null;
            }

            if (enabledTextColorDropDown != null) {
                enabledTextColorDropDown.Dispose ();
                enabledTextColorDropDown = null;
            }

            if (fontDropDown != null) {
                fontDropDown.Dispose ();
                fontDropDown = null;
            }

            if (letterSpacingDropDown != null) {
                letterSpacingDropDown.Dispose ();
                letterSpacingDropDown = null;
            }

            if (pressedTextColorDropDown != null) {
                pressedTextColorDropDown.Dispose ();
                pressedTextColorDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (rippleColorDropDown != null) {
                rippleColorDropDown.Dispose ();
                rippleColorDropDown = null;
            }

            if (stateSwitch != null) {
                stateSwitch.Dispose ();
                stateSwitch = null;
            }

            if (textSizeDropDown != null) {
                textSizeDropDown.Dispose ();
                textSizeDropDown = null;
            }

            if (themeDropDown != null) {
                themeDropDown.Dispose ();
                themeDropDown = null;
            }
        }
    }
}
