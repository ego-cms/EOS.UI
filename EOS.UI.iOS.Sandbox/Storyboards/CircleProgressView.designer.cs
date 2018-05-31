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
    [Register ("CircleProgressView")]
    partial class CircleProgressView
    {
        [Outlet]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        UIKit.UISwitch showProgressSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown alternativeColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown colorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown fontDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown textSizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (alternativeColorDropDown != null) {
                alternativeColorDropDown.Dispose ();
                alternativeColorDropDown = null;
            }

            if (colorDropDown != null) {
                colorDropDown.Dispose ();
                colorDropDown = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (fontDropDown != null) {
                fontDropDown.Dispose ();
                fontDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (showProgressSwitch != null) {
                showProgressSwitch.Dispose ();
                showProgressSwitch = null;
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