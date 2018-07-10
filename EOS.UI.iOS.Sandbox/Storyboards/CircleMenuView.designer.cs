// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Sandbox
{
	[Register ("CircleMenuView")]
	partial class CircleMenuView
	{
		[Outlet]
		UIKit.UILabel swipeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (swipeLabel != null) {
				swipeLabel.Dispose ();
				swipeLabel = null;
			}
		}
	}
}
