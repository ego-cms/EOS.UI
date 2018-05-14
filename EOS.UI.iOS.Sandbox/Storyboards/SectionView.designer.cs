// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    [Register ("SectionView")]
    partial class SectionView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView sectionTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (sectionTableView != null) {
                sectionTableView.Dispose ();
                sectionTableView = null;
            }
        }
    }
}