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
        UIKit.UITextField checkmarkColorField { get; set; }


        [Outlet]
        UIKit.UITextField colorField { get; set; }


        [Outlet]
        UIKit.UIView containerView { get; set; }


        [Outlet]
        UIKit.UITextField fontField { get; set; }


        [Outlet]
        UIKit.UIButton resetButton { get; set; }


        [Outlet]
        UIKit.UISwitch showProgressSwitch { get; set; }


        [Outlet]
        UIKit.UITextField textSizeField { get; set; }


        [Outlet]
        UIKit.UITextField themeField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (checkmarkColorField != null) {
                checkmarkColorField.Dispose ();
                checkmarkColorField = null;
            }

            if (colorField != null) {
                colorField.Dispose ();
                colorField = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (fontField != null) {
                fontField.Dispose ();
                fontField = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (showProgressSwitch != null) {
                showProgressSwitch.Dispose ();
                showProgressSwitch = null;
            }

            if (textSizeField != null) {
                textSizeField.Dispose ();
                textSizeField = null;
            }

            if (themeField != null) {
                themeField.Dispose ();
                themeField = null;
            }
        }
    }
}