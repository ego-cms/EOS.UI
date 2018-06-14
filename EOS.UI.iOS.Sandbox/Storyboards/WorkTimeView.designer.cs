// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using EOS.UI.iOS.Components;
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EOS.UI.iOS.Sandbox
{
    [Register ("WorkTimeView")]
    partial class WorkTimeView
    {
        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown currentDayBackgroundColorDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown currentDayDevidersColor { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown currentDayTextColorDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown dayEvenBackgroundColor { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown dayFontDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown dayTextColorDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown dayTextSizeDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown devidersColor { get; set; }


        [Outlet]
        UIKit.UIButton resetButton { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown themesDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown titleFontDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown titleSizeDropDown { get; set; }


        [Outlet]
        EOS.UI.iOS.Sandbox.EOSSandboxDropDown weekStartDropdown { get; set; }


        [Outlet]
        WorkTimeCalendarCollection workTimeCollection { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (currentDayBackgroundColorDropDown != null) {
                currentDayBackgroundColorDropDown.Dispose ();
                currentDayBackgroundColorDropDown = null;
            }

            if (currentDayDevidersColor != null) {
                currentDayDevidersColor.Dispose ();
                currentDayDevidersColor = null;
            }

            if (currentDayTextColorDropDown != null) {
                currentDayTextColorDropDown.Dispose ();
                currentDayTextColorDropDown = null;
            }

            if (dayEvenBackgroundColor != null) {
                dayEvenBackgroundColor.Dispose ();
                dayEvenBackgroundColor = null;
            }

            if (dayFontDropDown != null) {
                dayFontDropDown.Dispose ();
                dayFontDropDown = null;
            }

            if (dayTextColorDropDown != null) {
                dayTextColorDropDown.Dispose ();
                dayTextColorDropDown = null;
            }

            if (dayTextSizeDropDown != null) {
                dayTextSizeDropDown.Dispose ();
                dayTextSizeDropDown = null;
            }

            if (devidersColor != null) {
                devidersColor.Dispose ();
                devidersColor = null;
            }

            if (resetButton != null) {
                resetButton.Dispose ();
                resetButton = null;
            }

            if (themesDropDown != null) {
                themesDropDown.Dispose ();
                themesDropDown = null;
            }

            if (titleFontDropDown != null) {
                titleFontDropDown.Dispose ();
                titleFontDropDown = null;
            }

            if (titleSizeDropDown != null) {
                titleSizeDropDown.Dispose ();
                titleSizeDropDown = null;
            }

            if (weekStartDropdown != null) {
                weekStartDropdown.Dispose ();
                weekStartDropdown = null;
            }

            if (workTimeCollection != null) {
                workTimeCollection.Dispose ();
                workTimeCollection = null;
            }
        }
    }
}