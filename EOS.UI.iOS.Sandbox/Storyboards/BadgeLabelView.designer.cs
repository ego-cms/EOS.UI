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
        UIKit.UITextField backgroundColorField { get; set; }


        [Outlet]
        UIKit.UIView containerView { get; set; }


        [Outlet]
        UIKit.UITextField cornerRadiusField { get; set; }


        [Outlet]
        UIKit.UITextField fontColorField { get; set; }


        [Outlet]
        UIKit.UITextField fontField { get; set; }


        [Outlet]
        UIKit.UITextField fontSizeField { get; set; }


        [Outlet]
        UIKit.UITextField letterSpacingField { get; set; }


        [Outlet]
        UIKit.UIButton resetButton { get; set; }


        [Outlet]
        UIKit.UITextField themeField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backgroundColorField != null) {
                backgroundColorField.Dispose ();
                backgroundColorField = null;
            }

            if (containerView != null) {
                containerView.Dispose ();
                containerView = null;
            }

            if (cornerRadiusField != null) {
                cornerRadiusField.Dispose ();
                cornerRadiusField = null;
            }

            if (fontColorField != null) {
                fontColorField.Dispose ();
                fontColorField = null;
            }

            if (fontField != null) {
                fontField.Dispose ();
                fontField = null;
            }

            if (fontSizeField != null) {
                fontSizeField.Dispose ();
                fontSizeField = null;
            }

            if (letterSpacingField != null) {
                letterSpacingField.Dispose ();
                letterSpacingField = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (themeField != null) {
                themeField.Dispose ();
                themeField = null;
            }
        }
    }
}