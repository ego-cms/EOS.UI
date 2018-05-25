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
        UIKit.UITextField backgroundField { get; set; }

        [Outlet]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        UIKit.UITextField disabledField { get; set; }

        [Outlet]
        UIKit.UISwitch enableSwitch { get; set; }

        [Outlet]
        UIKit.UITextField pressedField { get; set; }

        [Outlet]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        UIKit.UITextField shadowField { get; set; }

        [Outlet]
        UIKit.UITextField sizeField { get; set; }

        [Outlet]
        UIKit.UITextField themeField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backgroundField != null) {
                backgroundField.Dispose ();
                backgroundField = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (disabledField != null) {
                disabledField.Dispose ();
                disabledField = null;
            }

            if (enableSwitch != null) {
                enableSwitch.Dispose ();
                enableSwitch = null;
            }

            if (pressedField != null) {
                pressedField.Dispose ();
                pressedField = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (shadowField != null) {
                shadowField.Dispose ();
                shadowField = null;
            }

            if (sizeField != null) {
                sizeField.Dispose ();
                sizeField = null;
            }

            if (themeField != null) {
                themeField.Dispose ();
                themeField = null;
            }
        }
    }
}