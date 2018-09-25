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
	[Register ("CircleMenuItemView")]
	partial class CircleMenuItemView
	{
		[Outlet]
		UIKit.UIImageView menuItemImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (menuItemImage != null) {
				menuItemImage.Dispose ();
				menuItemImage = null;
			}
		}
	}
}
