// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS
{
    [Register ("CircleProgress")]
    partial class CircleProgress
    {
        [Outlet]
        UIKit.UIView circleView { get; set; }


        [Outlet]
        UIKit.UIImageView imageView { get; set; }


        [Outlet]
        UIKit.UILabel percentLabel { get; set; }


        [Outlet]
        UIKit.UIView stopView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel percentSignLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (circleView != null) {
                circleView.Dispose ();
                circleView = null;
            }

            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (percentLabel != null) {
                percentLabel.Dispose ();
                percentLabel = null;
            }

            if (percentSignLabel != null) {
                percentSignLabel.Dispose ();
                percentSignLabel = null;
            }

            if (stopView != null) {
                stopView.Dispose ();
                stopView = null;
            }
        }
    }
}
