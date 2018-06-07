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
    [Register ("CTAButtonView")]
    partial class CTAButtonView
    {
        [Outlet]
        UIKit.UIView containerView { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown cornerRadiusDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown disabledBackgroundDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown disabledTextColorDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown enabledBackgroundDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown enabledTextColorDropDown { get; set; }


        [Outlet]
        UIKit.UISwitch enableSwitch { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown fontDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown letterSpacingDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown pressedBackgroundDropdown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown pressedTextColorDropDown { get; set; }


        [Outlet]
        UIKit.UIButton resetButton { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown textSizeDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (cornerRadiusDropDown != null) {
                cornerRadiusDropDown.Dispose ();
                cornerRadiusDropDown = null;
            }

            if (disabledBackgroundDropDown != null) {
                disabledBackgroundDropDown.Dispose ();
                disabledBackgroundDropDown = null;
            }

            if (disabledTextColorDropDown != null) {
                disabledTextColorDropDown.Dispose ();
                disabledTextColorDropDown = null;
            }

            if (enabledBackgroundDropDown != null) {
                enabledBackgroundDropDown.Dispose ();
                enabledBackgroundDropDown = null;
            }

            if (enabledTextColorDropDown != null) {
                enabledTextColorDropDown.Dispose ();
                enabledTextColorDropDown = null;
            }

            if (enableSwitch != null) {
                enableSwitch.Dispose ();
                enableSwitch = null;
            }

            if (fontDropDown != null) {
                fontDropDown.Dispose ();
                fontDropDown = null;
            }

            if (letterSpacingDropDown != null) {
                letterSpacingDropDown.Dispose ();
                letterSpacingDropDown = null;
            }

            if (pressedBackgroundDropdown != null) {
                pressedBackgroundDropdown.Dispose ();
                pressedBackgroundDropdown = null;
            }

            if (pressedTextColorDropDown != null) {
                pressedTextColorDropDown.Dispose ();
                pressedTextColorDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
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