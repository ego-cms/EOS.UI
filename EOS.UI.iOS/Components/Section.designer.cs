// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
            if (sectionButton != null) {
                sectionButton.Dispose ();
                sectionButton = null;
            }

            if (sectionName != null) {
                sectionName.Dispose ();
                sectionName = null;
            }

            if (paddingTop != null) {
                paddingTop.Dispose ();
                paddingTop = null;
            }

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
        }
    }
}
