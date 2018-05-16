// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Components
{
    [Register ("Section")]
    partial class Section
    {
        [Outlet]
        UIKit.NSLayoutConstraint paddingBottom { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint paddingLeft { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint paddingRight { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint paddingTop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton sectionButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel sectionName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (paddingBottom != null) {
                paddingBottom.Dispose ();
                paddingBottom = null;
            }

            if (paddingLeft != null) {
                paddingLeft.Dispose ();
                paddingLeft = null;
            }

            if (paddingRight != null) {
                paddingRight.Dispose ();
                paddingRight = null;
            }

            if (paddingTop != null) {
                paddingTop.Dispose ();
                paddingTop = null;
            }

            if (sectionButton != null) {
                sectionButton.Dispose ();
                sectionButton = null;
            }

            if (sectionName != null) {
                sectionName.Dispose ();
                sectionName = null;
            }
        }
    }
}