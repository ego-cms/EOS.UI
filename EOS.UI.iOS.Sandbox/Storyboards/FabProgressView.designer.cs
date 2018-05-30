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
    [Register ("FabProgressView")]
    partial class FabProgressView
    {
        [Outlet]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        UIKit.UISwitch enableSwitch { get; set; }

        [Outlet]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown backgroundDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown disabledColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown pressedColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown shadowDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown sizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backgroundDropDown != null) {
                backgroundDropDown.Dispose ();
                backgroundDropDown = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (disabledColorDropDown != null) {
                disabledColorDropDown.Dispose ();
                disabledColorDropDown = null;
            }

            if (enableSwitch != null) {
                enableSwitch.Dispose ();
                enableSwitch = null;
            }

            if (pressedColorDropDown != null) {
                pressedColorDropDown.Dispose ();
                pressedColorDropDown = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
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
        }
    }
}