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
    [Register("CircleProgressView")]
    partial class CircleProgressView
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown alternativeColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown colorDropDown { get; set; }

        [Outlet]
        UIKit.UIView containerView { get; set; }

        [Outlet]
        EOS.UI.iOS.Sandbox.CustomDropDown fillColorDropDown { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown fontDropDown { get; set; }

        [Outlet]
        UIKit.UIButton resetButton { get; set; }

        [Outlet]
        UIKit.UISwitch showProgressSwitch { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown textSizeDropDown { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        EOS.UI.iOS.Sandbox.CustomDropDown themeDropDown { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (containerView != null)
            {
                containerView.Dispose();
                containerView = null;
            }

            if (resetButton != null)
            {
                resetButton.Dispose();
                resetButton = null;
            }

            if (showProgressSwitch != null)
            {
                showProgressSwitch.Dispose();
                showProgressSwitch = null;
            }

            if (alternativeColorDropDown != null)
            {
                alternativeColorDropDown.Dispose();
                alternativeColorDropDown = null;
            }

            if (colorDropDown != null)
            {
                colorDropDown.Dispose();
                colorDropDown = null;
            }

            if (fontDropDown != null)
            {
                fontDropDown.Dispose();
                fontDropDown = null;
            }

            if (textSizeDropDown != null)
            {
                textSizeDropDown.Dispose();
                textSizeDropDown = null;
            }

            if (themeDropDown != null)
            {
                themeDropDown.Dispose();
                themeDropDown = null;
            }

            if (fillColorDropDown != null)
            {
                fillColorDropDown.Dispose();
                fillColorDropDown = null;
            }
        }
    }
}
