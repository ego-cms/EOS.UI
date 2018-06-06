// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    [Register ("SimpleButtonView")]
    partial class SimpleButtonView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown cornerRadiusDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown disabledBackgroundDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown disabledTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown enabledBackgrDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint enabledBackgroundDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown enabledTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch enableSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown fontDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown letterSpacingDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown pressedBackgroundDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown pressedTextColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.Controls.EOSSandboxButton resetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView scrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown textSizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }

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

            if (enabledBackgrDropDown != null) {
                enabledBackgrDropDown.Dispose ();
                enabledBackgrDropDown = null;
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

            if (pressedBackgroundDropDown != null) {
                pressedBackgroundDropDown.Dispose ();
                pressedBackgroundDropDown = null;
            }

            if (pressedTextColorDropDown != null) {
                pressedTextColorDropDown.Dispose ();
                pressedTextColorDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (scrollView != null) {
                scrollView.Dispose ();
                scrollView = null;
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