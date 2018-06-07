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
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        UIKit.UITableView controlsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (controlsTableView != null) {
                controlsTableView.Dispose ();
                controlsTableView = null;
            }
        }
    }
}