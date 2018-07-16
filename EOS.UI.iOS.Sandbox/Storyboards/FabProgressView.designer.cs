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
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown shadowColorDropDown { get; set; }


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
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown backgroundDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown disabledColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown pressedColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown sizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown themeDropDown { get; set; }

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

            if (shadowColorDropDown != null) {
                shadowColorDropDown.Dispose ();
                shadowColorDropDown = null;
            }

            if (shadowOffsetXDropDown != null) {
                shadowOffsetXDropDown.Dispose ();
                shadowOffsetXDropDown = null;
            }

            if (shadowOffsetYDropDown != null) {
                shadowOffsetYDropDown.Dispose ();
                shadowOffsetYDropDown = null;
            }

            if (shadowOpacityDropDown != null) {
                shadowOpacityDropDown.Dispose ();
                shadowOpacityDropDown = null;
            }

            if (shadowRadiusDropDown != null) {
                shadowRadiusDropDown.Dispose ();
                shadowRadiusDropDown = null;
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