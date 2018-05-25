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
    [Register ("BadgeLabelView")]
    partial class BadgeLabelView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown backgroundColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown cornerRadiusDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown fontDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown letterSpacingDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown textColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown textSizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backgroundColorDropDown != null) {
                backgroundColorDropDown.Dispose ();
                backgroundColorDropDown = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (cornerRadiusDropDown != null) {
                cornerRadiusDropDown.Dispose ();
                cornerRadiusDropDown = null;
            }

            if (fontDropDown != null) {
                fontDropDown.Dispose ();
                fontDropDown = null;
            }

            if (letterSpacingDropDown != null) {
                letterSpacingDropDown.Dispose ();
                letterSpacingDropDown = null;
            }

            if (letterSpacingDropDown != null) {
                letterSpacingDropDown.Dispose ();
                letterSpacingDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (textColorDropDown != null) {
                textColorDropDown.Dispose ();
                textColorDropDown = null;
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